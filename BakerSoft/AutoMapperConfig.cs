﻿using AutoMapper;
using BakerSoft.Models;
using DAL.Models;
using GSTBill.Models;

namespace BakerSoft
{
    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(
                config =>
                {
                    config.CreateMap<TAX_MASTER, Tax>()
                    .ForMember(dest => dest.TaxRate, opt => opt.MapFrom(src => src.TaxRate))
                    .ForMember(dest => dest.CGST, opt => opt.MapFrom(src => src.TaxRate))
                    .ForMember(dest => dest.SGST, opt => opt.MapFrom(src => src.TaxRate));

                    config.CreateMap<UOM_DEFINITION_MASTER, UomDefinitions>();

                    config.CreateMap<PRODUCT, Product>().
                    ForMember(dest => dest.UoMDefinitionList, opt => opt.MapFrom(src => src.UOM_CATEGORY_MASTER.UOM_DEFINITION_MASTER));

                    config.CreateMap<UOM_CATEGORY_MASTER, UomCategory>();

                    config.CreateMap<PRODUCT_CATEGORY_MASTER_NEW, ProductCategory>().
                    ForMember(dest => dest.TaxId, opt => opt.MapFrom(src => src.CATEGORY_TAX_DEFINITION_NEW.TaxId)).
                    ForMember(dest => dest.TaxRate, opt => opt.MapFrom(src => src.CATEGORY_TAX_DEFINITION_NEW.TAX_MASTER.TaxRate));

                    config.CreateMap<Supplier, SUPPLIER>();
                    config.CreateMap<SUPPLIER, Supplier>();

                    config.CreateMap<Address, ADDRESS>();
                    config.CreateMap<ADDRESS, Address>();

                    config.CreateMap<Product, Product>();
                    config.CreateMap<Product, PRODUCT>();

                    config.CreateMap<Payment, PAYMENT>();
                    config.CreateMap<PurchasePayment, PURCHASE_PAYMENTS>().
                    ForMember(dest => dest.PAYMENT, opt => opt.MapFrom(src => src.Payment));
                    config.CreateMap<PurchaseProduct, PURCHASE_PRODUCTS>();
                    //ForMember(dest => dest.PRODUCT, opt => opt.MapFrom(src => src.Product));
                    config.CreateMap<PurchaseTransaction, PURCHASE_TRANSACTIONS>().
                    ForMember(dest => dest.PURCHASE_PRODUCTS, opt => opt.MapFrom(src => src.ItemList)).
                    ForMember(dest => dest.PURCHASE_PAYMENTS, opt => opt.MapFrom(src => src.PaymentList));

                    config.CreateMap<SalePayment, SALES_PAYMENTS>().
                    ForMember(dest => dest.PAYMENT, opt => opt.MapFrom(src => src.Payment));
                    config.CreateMap<SaleProduct, SALES_PRODUCTS>();
                    config.CreateMap<SaleTransaction, SALE_TRANSACTIONS>().
                    ForMember(dest => dest.SALES_PRODUCTS, opt => opt.MapFrom(src => src.ItemList)).
                    ForMember(dest => dest.SALES_PAYMENTS, opt => opt.MapFrom(src => src.PaymentList));

                    config.CreateMap<SALE_TRANSACTIONS, SaleTransaction>();
                    config.CreateMap<PURCHASE_TRANSACTIONS, PurchaseTransaction>();

                    //.ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                    //.ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
                    //.ForMember(dest => dest.ProductDescription, opt => opt.MapFrom(src => src.ProductDescription))
                    //.ForMember(dest => dest.ProductCategoryId, opt => opt.MapFrom(src => src.ProductCategoryId))
                    //.ForMember(dest => dest.ProductSearchId, opt => opt.MapFrom(src => src.ProductSearchId))
                    //.ForMember(dest => dest.ProductUoM, opt => opt.MapFrom(src => src.ProductUoM))
                    //.ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType));
                });
        }
    }
}
