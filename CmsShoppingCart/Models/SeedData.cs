﻿using CmsShoppingCart.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace CmsShoppingCart.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new CmsShoppingCartContext
                (serviceProvider.GetRequiredService<DbContextOptions<CmsShoppingCartContext>>()))
            {
                // if db is already populated do nothing
                if (context.Pages.Any())
                {
                    return;
                }
                else
                {
                    context.Pages.AddRange(
                        new Page
                        {
                            Title = "Home",
                            Slug = "home",
                            Content = "home page",
                            Sorting = 0
                        },
                        new Page
                        {
                            Title = "About us",
                            Slug = "about-us",
                            Content = "about us page",
                            Sorting = 100
                        },
                        new Page
                        {
                            Title = "Services",
                            Slug = "services",
                            Content = "services page",
                            Sorting = 100
                        });
                    context.SaveChanges();
                }
            }
        }
    }
}
