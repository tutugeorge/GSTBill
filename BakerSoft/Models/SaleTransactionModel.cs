﻿using BakerSoft.Models;
using BakerSoft.Repositories;
using GSTBill.Definitions;
using GSTBill.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSTBill.Models
{
    class SaleTransactionModel //: Transaction
    {
        private ISaleTransactionRepository _saleTransactionRepository;

        //public int TransactionStatus { get; set; }
        //public double TransactionTotal { get; set; }
        //public double TransactionTaxTotal { get; set; }
        //public decimal TransactionDiscountTotal { get; set; }        
        //public List<Product> ItemList
        //{
        //    get;
        //    set;
        //}
        //public ObservableCollection<Payment> PaymentList { get; set; }

        public SaleTransaction sale { get; set; }

        public SaleTransactionModel(ISaleTransactionRepository saleTransactionRepository)
        {
            _saleTransactionRepository = saleTransactionRepository;            
        }

        public decimal AddPayment(SalePayment payment)
        {
            if (sale.PaymentList == null)
            {
                sale.PaymentList = new ObservableCollection<SalePayment>();
            }
            sale.PaymentList.Add(payment);

            return GetOutstandingAmount();
        }

        public void Complete()
        {
            //base.Complete();
            //Call Data Layer
            _saleTransactionRepository.InsertTransaction(sale);
            ClearTransaction();
        }

        public void Update()
        {
            //base.Update();
        }

        public void Cancel()
        {
            //base.Cancel();
            ClearTransaction();
        }

        public void AddItem(SaleProduct item)
        {
            //base.AddItem(item);
            if (sale == null)
                sale = new SaleTransaction();
            if (sale.ItemList == null)
                sale.ItemList = new List<SaleProduct>(); //new List<Product>();
            sale.ItemList.Add(item);

            AddToTransactionTotal(item.PriceList[0].Value, item.Quantity);
            AddToTaxTotal(item.ProductTax, item.PriceList[0].Value, item.Quantity);
        }

        public void RemoveItem(int itemIndex)
        {
            //TODO : Remove selected item from the list
            var item = sale.ItemList[itemIndex];
            sale.ItemList.RemoveAt(itemIndex);
            //SubstractFromTransactionTotal(item.PriceList[0].Value, item.Quantity);
            //SubstractFromTaxTotal(item.ProductTax, item.PriceList[0].Value, item.Quantity);
        }

        private void AddToTaxTotal(Tax tax, decimal price, decimal quantity)
        {
            sale.TransactionTaxTotal = sale.TransactionTaxTotal +
                                    ((price * tax.SGST) + 
                                    (price * tax.CGST)) * quantity;
        }

        private void SubstractFromTaxTotal(Tax tax, Price price, int quantity)
        {
            sale.TransactionTaxTotal = sale.TransactionTaxTotal - 
                                    ((price.SellingPrice * tax.SGST) +
                                    (price.SellingPrice * tax.CGST)) * quantity;
        }

        private void AddToTransactionTotal(decimal price, decimal quantity)
        {
            sale.TransactionTotal = sale.TransactionTotal + Convert.ToDecimal(price * quantity);
        }

        private void SubstractFromTransactionTotal(Price price, int quantity)
        {
            sale.TransactionTotal = sale.TransactionTotal - (price.SellingPrice * quantity);
        }

        private void ClearTransaction()
        {
            sale.ItemList.Clear();
            sale.TransactionTotal = 0.00m;
            sale.TransactionTaxTotal = 0.00m;
        }

        private decimal GetPaymentTotal()
        {
            decimal paymentTotal = 0.0m;
            foreach(var item in sale.PaymentList)
            {
                paymentTotal += item.PaymentAmount;
            }
            return paymentTotal;
        }

        private decimal GetOutstandingAmount()
        {
            return (sale.TransactionTotal - GetPaymentTotal());
        }
    }
}
