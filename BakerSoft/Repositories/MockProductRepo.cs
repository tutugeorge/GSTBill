﻿using BakerSoft.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSTBill.Models;

namespace BakerSoft.Repositories
{
    class MockProductRepo : IProductRepository
    {
        public void AddProduct()
        {
            throw new NotImplementedException();
        }

        public List<SaleItem> GetProductsById(string id)
        {
            throw new NotImplementedException();
        }

        public List<SaleItem> GetProductsByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
