namespace PcPick.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PcPick.MyDB.MyDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "PcPick.MyDB.MyDbContext";
        }

        protected override void Seed(PcPick.MyDB.MyDbContext context)
        {
            #region Categories
            context.Categories.AddOrUpdate(x => x.CategoryId,
                new Models.Category
                {
                    CategoryId = 1,
                    Name = "Grafikkort / GUP"
                },
                new Models.Category
                {
                    CategoryId = 2,
                    Name = "Processor / CPU"
                });
            #endregion

            #region Product with CategoryId 1
            context.Products.AddOrUpdate(x => x.ProductId,
                new Models.Product
                {
                    ProductId = 1,
                    Name = "ASUS GeForce GTX 1050 Ti 4GB STRIX GAMING",
                    Description = "Kraftfullt grafikkort med 4GB minne, anpassad för den som vill få full upplevelse utav sina spel.",
                    Price = 2349,
                    CategoryId = 1
                },
                new Models.Product
                {
                    ProductId = 2,
                    Name = "ASUS GeForce GTX 1060 6GB DUAL OC",
                    Description = "GeForce GTX 1060 grafikkort är laddade med innovativa nya speltekniker som gör det till ett perfekt val för de senaste HD- spelen.",
                    Price = 3199,
                    CategoryId = 1
                });
            #endregion

            #region Product with CategoryId 2
            context.Products.AddOrUpdate(x => x.ProductId,
                new Models.Product
                {
                    ProductId = 3,
                    Name = "AMD Ryzen 7 1700X 3.8 GHz 20MB",
                    Description = "1700X är en processor ur den nya Ryzen-arkitekturen från AMD. " +
                    "Den är en modell ur R7-serien, vilket innebär att den har 8 kärnor och 16 trådar genom SMT (Simultaneous Multi Threading).",
                    Price = 2490,
                    CategoryId = 2
                });
            #endregion
        }
    }
}
