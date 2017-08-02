﻿using BakerSoft.Models;
using GSTBill.Models;
using GSTBill.ViewModels;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakerSoft.ViewModels
{
    class AddProductViewModel : BaseViewModel
    {
        private ProductModel _productModel;

        public string ProductSearchId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string SellingPrice { get; set;}
        public Tax ProductTax { get; set; }
        public List<Price> PriceList { get; set; }

        //Add other data capture items for product
        public DelegateCommand AddProductCmd { get; set; }

        public AddProductViewModel(ProductModel productModel)
        {
            _productModel = productModel;
            AddProductCmd = new DelegateCommand(AddProduct);
        }

        private void AddProduct()
        {
            var product = new Product();
            product.Name = ProductName;
            product.SearchId = Convert.ToInt32(ProductSearchId);
            //product.Tax = 
            _productModel.Add();
        }
    }
}
