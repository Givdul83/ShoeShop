using Infrastructure.ProductContext;
using Infrastructure.ProductEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;

public class ProductImagesRepository : ProductsBaseRepo<ProductImage, ProductDbContext>

{
    private readonly ProductDbContext _context;

    public ProductImagesRepository(ProductDbContext context) : base(context)
    {
        _context = context;
    }

    
    
}


