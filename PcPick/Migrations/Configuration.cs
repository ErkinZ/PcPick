namespace PcPick.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Drawing;
    using System.IO;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PcPick.MyDB.MyDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "PcPick.MyDB.MyDbContext";
        }

        public byte[] ImageToArray(string path)
        {
            Image img = Image.FromFile(path);
            byte[] arr;
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                arr = ms.ToArray();
            }
            return arr;
        }

        protected override void Seed(PcPick.MyDB.MyDbContext context)
        {
            #region Categories
            context.Categories.AddOrUpdate(x => x.CategoryId,
                new Models.Category
                {
                    CategoryId = 1,
                    Name = "Graphic card / GUP"
                },
                new Models.Category
                {
                    CategoryId = 2,
                    Name = "Processor / CPU"
                },
                new Models.Category
                {
                    CategoryId = 3,
                    Name = "Harddrive"
                },
                new Models.Category
                {
                    CategoryId = 4,
                    Name = "Motherboard"
                });
            #endregion

            #region Product with CategoryId 1
            context.Products.AddOrUpdate(x => x.ProductId,
                new Models.Product
                {
                    ProductId = 1,
                    Name = "ASUS GeForce GTX 1050 Ti 4GB STRIX GAMING",
                    Description = "Turn your PC into a true gaming machine with the GeForce GTX 1050 Ti graphics card. " +
                    "It's based on the state-of-the-art Pascal architecture manufactured with the ultra-fast FinFET, " +
                    "and comes with 768 CUDA Cores and 4GB 128-bit GDDR5 memory.",
                    Price = 2349,
                    Photo = ImageToArray(AppDomain.CurrentDomain.BaseDirectory + @"Images\AsusGTX1050TI.jpg"),
                    CategoryId = 1
                },
                new Models.Product
                {
                    ProductId = 2,
                    Name = "NVIDIA Titan RTX 24GB",
                    Description = "The NVIDIA TITAN RTX is the fastest and most powerful PC graphics card ever built. " +
                    "It’s powered by the award-winning Turing architecture, bringing 130 Tensor TFLOPs of performance, 576 tensor cores," +
                    " and 24 GB of ultra-fast GDDR6 memory to your PC.",
                    Price = 28990,
                    Photo = ImageToArray(AppDomain.CurrentDomain.BaseDirectory + @"Images\NVIDIATitanRTX24GB.jpg"),
                    CategoryId = 1
                },
                new Models.Product
                {
                    ProductId = 3,
                    Name = "MSI GeForce RTX 2060 6GB VENTUS OC",
                    Description = "TORX Fan 2.0: Dispersion fan blade: Steep curved blade accelerating the airflow. " +
                    "Traditional fan blade: Provides steady airflow to massive heat sink below. " +
                    "Afterburner Overclocking Utility: Wireless control through Android / iOS devices. " +
                    "Predator: In - game video recording. " +
                    "Kombustor: DirectX12 benchmark. Supports multi - GPU setups.",
                    Price = 4199,
                    Photo = ImageToArray(AppDomain.CurrentDomain.BaseDirectory + @"Images\MSIGeForceRTX2060.jpg"),
                    CategoryId = 1
                },
                new Models.Product
                {
                    ProductId = 4,
                    Name = "ASUS GeForce GTX 1060 6GB DUAL OC",
                    Description = "Double Ball Bearings give the unique MSI TORX 2.0 Fans a strong and lasting core for years of smooth gaming. " +
                    "They also remain virtually silent while spinning under load, keeping your graphics card cool during intense and lengthy gaming sessions.",
                    Price = 14999,
                    Photo = ImageToArray(AppDomain.CurrentDomain.BaseDirectory + @"Images\ASUSGeForceGTX10606GBDUALOC.jpg"),
                    CategoryId = 1
                },
                new Models.Product
                {
                    ProductId = 5,
                    Name = "MSI GeForce GTX 1060 6GB Gaming X",
                    Description = "GeForce GTX graphics cards are the most advanced ever created. " +
                    "Discover unprecedented performance, power efficiency, and next-generation gaming experiences.",
                    Price = 3199,
                    Photo = ImageToArray(AppDomain.CurrentDomain.BaseDirectory + @"Images\MSIGeForceGTX10606GBGamingX.jpg"),
                    CategoryId = 1
                },
                new Models.Product
                {
                    ProductId = 6,
                    Name = "EVGA GeForce GTX 1060 6GB SC Gaming",
                    Description = "The EVGA GeForce GTX 1060 is loaded with innovative gaming technologies, " +
                    "making it the perfect choice for the latest high-definition games. " +
                    "Powered by NVIDIA Pascal - the most advanced GPU architecture ever created - " +
                    "the GeForce GTX 1060 delivers brilliant performance that opens the door to virtual reality and beyond.",
                    Price = 3999,
                    Photo = ImageToArray(AppDomain.CurrentDomain.BaseDirectory + @"Images\EVGAGeForceGTX10606GBSCGaming.jpg"),
                    CategoryId = 1
                }
                );
            #endregion

            #region Product with CategoryId 2
            context.Products.AddOrUpdate(x => x.ProductId,
                new Models.Product
                {
                    ProductId = 7,
                    Name = "AMD Ryzen 7 1700X 3.8 GHz 20MB",
                    Description = "1700X is a powerful processor from AMD. 8 cores and 16 threads fit for heavy gaming!",
                    Price = 2490,
                    Photo = ImageToArray(AppDomain.CurrentDomain.BaseDirectory + @"Images\AMDRyzen71700X.jpg"),
                    CategoryId = 2
                },
                new Models.Product
                {
                    ProductId = 8,
                    Name = "Intel Core i5 9600K 3.7 GHz 9MB",
                    Description = "It used to be that Core i5 processors represented the best choice for mainstream users looking for value-oriented pricing, " +
                    "high performance, and modest power consumption. But now, fast Ryzen 5 CPUs often prove superior.",
                    Price = 3090,
                    Photo = ImageToArray(AppDomain.CurrentDomain.BaseDirectory + @"Images\IntelCorei59600K3.7GHz9MB.jpg"),
                    CategoryId = 2
                },
                new Models.Product
                {
                    ProductId = 9,
                    Name = "AMD Ryzen Threadripper 2990WX 3.0GHz 80MB",
                    Description = "32 cores provide an astonishing 64 threads of simultaneous multi-processing power, " +
                    "while 80MB of combined cache and vast I/O from the enthusiast-grade AMD X399 platform work together to enable the world’s most powerful desktop processor.",
                    Price = 19990,
                    Photo = ImageToArray(AppDomain.CurrentDomain.BaseDirectory + @"Images\AMDRyzenThreadripper2990WX3.0GHz80MB.jpg"),
                    CategoryId = 2
                },
                new Models.Product
                {
                    ProductId = 10,
                    Name = "Intel Core i7 8700K 3.7 GHz 12MB",
                    Description = "Core i7-8700K serves as this generation's flagship, sporting six Hyper-Threaded cores. " +
                    "Already, that's a big increase from Kaby Lake's 4C/8T maximum. " +
                    "It features the company's highest clock rates, accelerating up to 4.7 GHz via Turbo Boost. " +
                    "The -8700K does sacrifice some base frequency in exchange for a higher core count, though. " +
                    "Its 3.7 GHz specification is 500 MHz lower than the -7700K, " +
                    "offsetting the increased power consumption and heat generated by a 6C/12T configuration.",
                    Price = 4879,
                    Photo = ImageToArray(AppDomain.CurrentDomain.BaseDirectory + @"Images\IntelCorei78700K3.7GHz12MB.jpg"),
                    CategoryId = 2
                },
                new Models.Product
                {
                    ProductId = 11,
                    Name = "Intel Core i7-9800X 3.8 GHz 16,5MB",
                    Description = "The latest Intel Core i7 9800X X Series processor is powered by 8 Cores and 16 Threads to address the simultaneous, " +
                    "compute-intensive demands of creators.",
                    Price = 6999,
                    Photo = ImageToArray(AppDomain.CurrentDomain.BaseDirectory + @"Images\IntelCorei7-9800X3.8GHz16,5MB.jpg"),
                    CategoryId = 2
                });
            #endregion

            #region Product with CategoryId 3
            context.Products.AddOrUpdate(x => x.ProductId,
                new Models.Product
                {
                    ProductId = 12,
                    Name = "Samsung 970 EVO 1TB",
                    Description = "Accelerate into the next generation of computing with the Samsung 970 EVO. " +
                    "Known to deliver breakthrough speeds, best-in-class reliability, and a broad range of capacity options up to 2TB.",
                    Price = 2399,
                    Photo = ImageToArray(AppDomain.CurrentDomain.BaseDirectory + @"Images\Samsung970EVO1TB.jpg"),
                    CategoryId = 3
                },
                new Models.Product
                {
                    ProductId = 13,
                    Name = "Samsung 860 EVO 1TB M.2",
                    Description = "The Samsung SSD 860 EVO Series is the newest of Samsung’s Client-PC SATA SSDs, " +
                    "specially designed for mainstream desktops and laptops. " +
                    "Building on the reputation of the Samsung SSD 850 EVO, " +
                    "the world’s first line of consumer SSDs with V-NAND 3bit MLC technology, " +
                    "the new Samsung SSD 860 EVO drives achieve industry-leading performance for SATA SSDs, " +
                    "offering improvements in speed, reliability, compatibility and capacity.",
                    Price = 1999,
                    Photo = ImageToArray(AppDomain.CurrentDomain.BaseDirectory + @"Images\Samsung860EVO1TBM.2.jpg"),
                    CategoryId = 3
                },
                new Models.Product
                {
                    ProductId = 14,
                    Name = "Seagate BarraCuda 2TB 7200rpm 256MB",
                    Description = "Versatile HDDs for all your PC needs bring you industry-leading excellence in personal computing. " +
                    "For over 20 years the BarraCuda family has delivered super - reliable storage for the hard drive industry. " +
                    "BarraCuda leads the market with the widestrange of storage options available.",
                    Price = 849,
                    Photo = ImageToArray(AppDomain.CurrentDomain.BaseDirectory + @"Images\SeagateBarraCuda2TB7200rpm256MB.jpg"),
                    CategoryId = 3
                },
                new Models.Product
                {
                    ProductId = 15,
                    Name = "Seagate Ironwolf 8TB 7200rpm 256MB",
                    Description = "The 8TB Seagate IronWolf is a high-quality HDD built for NAS storage that delivers incredible" +
                    " performance thanks to its easy to use 3.5 inch form factor that is just 26.1mm in height, the drive runs at 2700 rpm, " +
                    "the IronWolf's technical highlight is it’s 6Gb/s interface, which is the latest SATA III standard." +
                    " This drive in particular has a storage capacity of 8000GB with a 256MB Cache.",
                    Price = 2799,
                    Photo = ImageToArray(AppDomain.CurrentDomain.BaseDirectory + @"Images\SeagateIronwolf8TB7200rpm256MB.jpg"),
                    CategoryId = 3
                });
            #endregion

            #region Product with CategoryId 4
            context.Products.AddOrUpdate(x => x.ProductId,
                new Models.Product
                {
                    ProductId = 16,
                    Name = "MSI Z390-A PRO",
                    Description = "MSI Pro series motherboards combine quality you can rely on, " +
                    "with top performance and clever business solutions. These are the key aspects of the MSI PRO SERIES motherboards.",
                    Price = 1590,
                    Photo = ImageToArray(AppDomain.CurrentDomain.BaseDirectory + @"Images\MSIZ390-APRO.jpg"),
                    CategoryId = 4
                },
                new Models.Product
                {
                    ProductId = 17,
                    Name = "ASUS ROG STRIX X470-I GAMING",
                    Description = "ROG STRIX X470-I GAMING is the perfect fusion of performance and ultra-compact panache: " +
                    "a Mini ITX motherboard for those who demand great gaming experiences with refined style. " +
                    "Whether you like to lurk in the shadows or light up the night,",
                    Price = 2913,
                    Photo = ImageToArray(AppDomain.CurrentDomain.BaseDirectory + @"Images\ASUSROGSTRIXX470-IGAMING.jpg"),
                    CategoryId = 4
                },
                new Models.Product
                {
                    ProductId = 18,
                    Name = "Gigabyte Z390 AORUS XTREME",
                    Description = "To unleash the full potential of the new Intel 8-core CPU, " +
                    "the motherboard requires the best CPU power design. " +
                    "With the best quality components and GIGABYTE R&D design capability, " +
                    "Z390 AORUS XTREME is a true beast among motherboards.",
                    Price = 6799,
                    Photo = ImageToArray(AppDomain.CurrentDomain.BaseDirectory + @"Images\GigabyteZ390AORUSXTREME.jpg"),
                    CategoryId = 4
                },
                new Models.Product
                {
                    ProductId = 19,
                    Name = "Gigabyte Z390 M GAMING",
                    Description = "Experience the extreme GIGABYTE Z390 GAMING X motherboard from the exclusive Z390 series. " +
                    "Bursting with exciting and patented GIGABYTE technology, " +
                    "ranging from audio to performance and even multi-way graphics for gaming " +
                    "enthusiasts who demand the highest frame rates without compromising on resolution.",
                    Price = 1690,
                    Photo = ImageToArray(AppDomain.CurrentDomain.BaseDirectory + @"Images\GigabyteZ390MGAMING.jpg"),
                    CategoryId = 4
                });
            #endregion
        }
    }
}
