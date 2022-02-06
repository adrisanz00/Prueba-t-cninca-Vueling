using System.Globalization;
using System.Xml;
using Web.Models;
using WebApplication2.Data;
using WebApplication2.ErrorHandling;

namespace WebApplication2.Utils
{
    public static class Helpers
    {
        public static void InsertDbCurrencies(DataContext _context)
        {
            List<ModelCurrency> currencies = GetCurrenciesXML();
            if (currencies.Count > 0)
            {
                foreach (var currency in _context.Currencies)
                {
                    _context.Currencies.Remove(currency);
                }

                _context.SaveChanges();

                foreach (var currency in currencies)
                {

                    _context.Currencies.Add(currency);
                }
                _context.SaveChanges();
            }
        }
        private static List<ModelCurrency> GetCurrenciesXML()
        {
            XmlTextReader readerRate = new XmlTextReader("http://quiet-stone-2094.herokuapp.com/ratedfs.xml");

            List<ModelCurrency> list = new List<ModelCurrency>();
            try
            {
                while (readerRate.Read())
                {
                    ModelCurrency rate = new ModelCurrency();
                    switch (readerRate.NodeType)
                    {
                        case XmlNodeType.Element: // The node is an element.
                            Console.Write("<" + readerRate.Name);

                            while (readerRate.MoveToNextAttribute()) // Read the attributes.
                                switch (readerRate.Name)
                                {
                                    case "from":
                                        rate.From = readerRate.Value;
                                        break;
                                    case "to":
                                        rate.To = readerRate.Value;
                                        break;
                                    case "rate":
                                        rate.Rate = double.Parse(readerRate.Value, CultureInfo.InvariantCulture); ;
                                        break;
                                    case "":
                                        break;
                                }

                            list.Add(rate);
                            break;
                    }
                }
                list.RemoveAt(0);
            }
            catch (Exception ex)
            {
                StreamWriter w = new StreamWriter("Log/error.txt");
                ErrorLog.save(ex.Message, w);
            }
            return list;
        }
        public static void InsertDbTransactions(DataContext _context)
        {
            List<ModelTransaction> transactions = GetTransactionsXML();
            if (transactions.Count > 0)
            {
                foreach (var item in _context.Transacciones)
                {
                    _context.Transacciones.Remove(item);
                }

                _context.SaveChanges();

                foreach (var transaction in transactions)
                {

                    _context.Transacciones.Add(transaction);
                }
                _context.SaveChanges();
            }
        }
        public static List<ModelTransaction> GetTransactionsXML()
        {
            XmlTextReader readerTransaction = new XmlTextReader("http://quiet-stone-2094.herokuapp.com/transactions.xml");

            List<ModelTransaction> listTransaction = new List<ModelTransaction>();
            try
            {
                while (readerTransaction.Read())
                {
                    ModelTransaction transaction = new ModelTransaction();
                    switch (readerTransaction.NodeType)
                    {
                        case XmlNodeType.Element: // The node is an element.

                            while (readerTransaction.MoveToNextAttribute()) // Read the attributes.

                                switch (readerTransaction.Name)
                                {
                                    case "sku":
                                        transaction.SKU = readerTransaction.Value;
                                        break;
                                    case "amount":
                                        transaction.Amount = double.Parse(readerTransaction.Value, CultureInfo.InvariantCulture);
                                        break;
                                    case "currency":
                                        transaction.Currency = readerTransaction.Value;
                                        break;
                                    case "":
                                        break;
                                }

                            listTransaction.Add(transaction);
                            break;
                    }
                }
                listTransaction.RemoveAt(0);
            }
            catch (Exception ex)
            {
                StreamWriter w = new StreamWriter("Log/error.txt");
                ErrorLog.save(ex.Message, w);
            }

            return listTransaction;
        }
        public static double CalculateTotalAmount(List<ModelTransaction> transformatedTransactions)
        {
            double totalAmount = 0;
            foreach (var transaction in transformatedTransactions)
            {
                totalAmount += transaction.Amount;
            }
            return Math.Round(totalAmount, 2);
        }
       
    }
}
