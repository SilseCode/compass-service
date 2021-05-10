using System;
using System.Collections.Generic;
using System.Linq;
using CompassSite.Database.Models;

public class PageInfo
{
    public int CategoryId { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages
    {
        get { return (int)Math.Ceiling((decimal)TotalItems / PageSize); }
    }

    public PageInfo(IEnumerable<Product> products)
    {
        TotalItems = products.Count();
        PageSize = 5;
    }
}