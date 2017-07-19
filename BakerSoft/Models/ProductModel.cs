﻿using BakerSoft.Definitions;
using BakerSoft.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSTBill.Models
{
    class ProductModel
    {
        private IProductRepository _productRepository;

        public ProductModel(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public List<Product> SearchByName(string name)
        {
            return _productRepository.GetProductsByName(name);
        }

        public List<Product> SearchById(string id)
        {
            return _productRepository.GetProductsById(id);
        }

        public void Add()
        {

        }

        public void Update()
        {

        }

        public void Delete()
        {
            
        }
    }
}