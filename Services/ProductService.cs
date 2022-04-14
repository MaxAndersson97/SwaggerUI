using Swagger.Models;
using System.Collections.Generic;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using _NetCore.DTO;
using System;
using IdentityServer4.Events;

namespace Swagger.Services
{
    public class ProductService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Products> _collection;

        public ProductService(IMapper mapper, IConfiguration configuration)
        {
            _mapper = mapper;
            var Client = new MongoClient(configuration.GetConnectionString("DB"));
            var database = Client.GetDatabase("stocko_db");

            _collection = database.GetCollection<Products>("Products");
        }
        public Func<string, string> greet = (name) => { return $"Hello, {name}!"; };

        public GetProducts GetProducts(Pagination pagination)
        {
            var getProducts = new GetProducts();
            if (pagination != null)
            {
                getProducts.Products = _collection.Find(Products => true).Skip((pagination.PageNumber - 1) * pagination.PageSize).Limit(pagination.PageSize).ToList();
            }
            else
            {
                getProducts.Products = _collection.Find(Products => true).ToList();
            }

            getProducts.Total = _collection.Find(Products => true).CountDocuments();
            return getProducts;
        }


        public Products GetProduct(string id) => _collection.Find(Products => Products.id == id).FirstOrDefault();

        public Products PostProduct(Products Products)
        {
            _collection.InsertOne(Products);
            return Products;
        }

        public Products PutProduct(string id, UpdateProduct Products)
        {
            var getProduct = _collection.Find(p => p.id == id).FirstOrDefault();
            var finalProduct = _mapper.Map<UpdateProduct, Products>(Products, getProduct);
            _collection.ReplaceOne(Products => Products.id == id, finalProduct);
            return finalProduct;
        }

        public Products DeleteProduct(string id)
        {
            var Products = _collection.Find(Products => Products.id == id).FirstOrDefault();
            _collection.DeleteOne(Products => Products.id == id);
            return Products;
        }

        public bool DeleteMultipleProducts(string[] ids)
        {
            var result = _collection.DeleteMany(Products => ids.Contains(Products.id));
            return result.DeletedCount > 0;
        }

        public GetProducts GetFilterProducts(FilterData filterData)
        {
            var builder = Builders<Products>.Filter;
            var filter = Builders<Products>.Filter.Empty;

            if (filterData.codeProduct != null && filterData.codeProduct != "")
            {
                filter = filter & builder.Eq(x => x.code, filterData.codeProduct);
            }
            if (filterData.productName != null && filterData.productName != "")
            {
                filter = filter & builder.Eq(x => x.name, filterData.productName);
            }
            if (filterData.categoryId != null && filterData.categoryId != "")
            {
                filter = filter & builder.Eq(x => x.category_id, filterData.categoryId);
            }
            if (filterData.brandId != null && filterData.brandId != "")
            {
                filter = filter & builder.Eq(x => x.brand_id, filterData.brandId);
            }

            var getProducts = new GetProducts();
            if (filterData.pagination != null)
            {
                getProducts.Products = _collection.Find(filter).Skip((filterData.pagination.PageNumber - 1) * filterData.pagination.PageSize).Limit(filterData.pagination.PageSize).ToList();
            }
            else
            {
                getProducts.Products = _collection.Find(filter).ToList();
            }

            getProducts.Total = _collection.Find(filter).CountDocuments();
/*
            if (filter.pagination != null)
            {
                getProducts.Products = _collection.Find(Products => ((filter.codeProduct == null || filter.codeProduct == "") ? true : (filter.codeProduct == Products.code)) &&
                    ((filter.productName == null || filter.productName == "") ? true : (filter.productName == Products.name)) &&
                    ((filter.categoryId == null || filter.categoryId == "") ? true : (filter.categoryId == Products.category_id)) &&
                    ((filter.brandId == null || filter.brandId == "") ? true : (filter.brandId == Products.brand_id))
                ).Skip((filter.pagination.PageNumber - 1) * filter.pagination.PageSize).Limit(filter.pagination.PageSize).ToList();
            }
            else
            {
                getProducts.Products = _collection.Find(Products => ((filter.codeProduct == null || filter.codeProduct == "") ? true : (filter.codeProduct == Products.code)) &&
                    ((filter.productName == null || filter.productName == "") ? true : (filter.productName == Products.name)) &&
                    ((filter.categoryId == null || filter.categoryId == "") ? true : (filter.categoryId == Products.category_id)) &&
                    ((filter.brandId == null || filter.brandId == "") ? true : (filter.brandId == Products.brand_id))
                ).ToList();
            }

            getProducts.Total = _collection.Find(Products => ((filter.codeProduct == null || filter.codeProduct == "") ? true : (filter.codeProduct == Products.code)) &&
                ((filter.productName == null || filter.productName == "") ? true : (filter.productName == Products.name)) &&
                ((filter.categoryId == null || filter.categoryId == "") ? true : (filter.categoryId == Products.category_id)) &&
                ((filter.brandId == null || filter.brandId == "") ? true : (filter.brandId == Products.brand_id))
            ).CountDocuments();*/
            return getProducts;
/*            return _collection.Find(Products => (
                ((filter.codeProduct == null || filter.codeProduct == "") ? true : (filter.codeProduct == Products.code)) &&
                ((filter.productName == null || filter.productName == "") ? true : (filter.productName == Products.name)) &&
                ((filter.categoryId == null || filter.categoryId == "") ? true : (filter.categoryId == Products.category_id)) &&
                ((filter.brandId == null || filter.brandId == "") ? true : (filter.brandId == Products.brand_id))
            )).ToList();*/
        }

        public bool UpdateProductImages(UpdateImages UpdateImages)
        {
            var product = _collection.Find(Products => Products.id == UpdateImages.id).FirstOrDefault();
            if (product != null)
            {
                //edit
                if (product.product_Images != null && product.product_Images.Count > 0)
                {
                    var ids = UpdateImages.product_Images.Select(m => m.id).ToList();
                    //take those product images which is need to be deleted
                    var deleteImages = product.product_Images.RemoveAll(m => !ids.Contains(m.id));

                    foreach (var item in UpdateImages.product_Images)
                    {
                        var image = product.product_Images.FirstOrDefault(m => m.id == item.id);
                        if (image != null)
                            image.isMain = item.isMain;
                    }

                    //tale remaining items
                    var remainingImages = UpdateImages.product_Images.Where(m => !product.product_Images.Select(m => m.id).Contains(m.id)).ToList();
                    product.product_Images.AddRange(remainingImages);
                }
                //add
                if (product.product_Images == null || product.product_Images.Count == 0)
                {
                    product.product_Images = new List<product_images>();
                    product.product_Images.AddRange(UpdateImages.product_Images);
                }
            }
            _collection.ReplaceOne(Products => Products.id == UpdateImages.id, product);
            return true;
        }

    }
}