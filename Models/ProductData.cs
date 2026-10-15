using System.Collections.Generic;

namespace PEESEE_GAMING.Models
{
    public static class ProductData
    {
        public static List<Product> GetProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Id = "LT01",
                    Name = "ASUS Vivobook 15",
                    Category = "laptop",
                    Brand = "ASUS",
                    Series = "Vivobook",
                    Price = 14990000m,
                    OldPrice = 16990000m,
                    Discount = 12,
                    Image = "images/products/asus-vivobook-15.jpg",
                    Rating = 4.7m,
                    Sold = 125,
                    Stock = 15,
                    Description = "Laptop văn phòng mỏng nhẹ, phù hợp học tập và làm việc.",
                    Specifications = new Dictionary<string, string>
                    {
                        { "cpu", "Intel Core i5-1335U" },
                        { "ram", "16GB" },
                        { "storage", "512GB SSD" },
                        { "screen", "15.6 inch" },
                        { "resolution", "Full HD" }
                    }
                },
                new Product
                {
                    Id = "LT02",
                    Name = "ACER Aspire 5",
                    Category = "laptop",
                    Brand = "ACER",
                    Series = "Aspire",
                    Price = 13990000m,
                    OldPrice = 15990000m,
                    Discount = 13,
                    Image = "images/products/acer-aspire-5.jpg",
                    Rating = 4.6m,
                    Sold = 98,
                    Stock = 12,
                    Description = "Laptop học tập và văn phòng với thiết kế hiện đại.",
                    Specifications = new Dictionary<string, string>
                    {
                        { "cpu", "Intel Core i5-12450H" },
                        { "ram", "16GB" },
                        { "storage", "512GB SSD" },
                        { "screen", "15.6 inch" },
                        { "resolution", "Full HD" }
                    }
                },
                new Product
                {
                    Id = "LT03",
                    Name = "DELL Inspiron 15",
                    Category = "laptop",
                    Brand = "DELL",
                    Series = "Inspiron",
                    Price = 15990000m,
                    OldPrice = 17990000m,
                    Discount = 11,
                    Image = "images/products/dell-inspiron-15.jpg",
                    Rating = 4.8m,
                    Sold = 86,
                    Stock = 10,
                    Description = "Laptop Dell bền bỉ dành cho học tập và công việc.",
                    Specifications = new Dictionary<string, string>
                    {
                        { "cpu", "Intel Core i5-1335U" },
                        { "ram", "16GB" },
                        { "storage", "512GB SSD" },
                        { "screen", "15.6 inch" },
                        { "resolution", "Full HD" }
                    }
                },
                new Product
                {
                    Id = "LT04",
                    Name = "Lenovo IdeaPad Slim 5",
                    Category = "laptop",
                    Brand = "LENOVO",
                    Series = "IdeaPad",
                    Price = 16990000m,
                    OldPrice = 18990000m,
                    Discount = 10,
                    Image = "images/products/lenovo-ideapad-slim-5.jpg",
                    Rating = 4.8m,
                    Sold = 74,
                    Stock = 9,
                    Description = "Laptop Lenovo mỏng nhẹ với hiệu năng ổn định.",
                    Specifications = new Dictionary<string, string>
                    {
                        { "cpu", "AMD Ryzen 7 7730U" },
                        { "ram", "16GB" },
                        { "storage", "512GB SSD" },
                        { "screen", "15.6 inch" },
                        { "resolution", "Full HD" }
                    }
                },
                new Product
                {
                    Id = "LG01",
                    Name = "ASUS ROG Strix G16",
                    Category = "laptop-gaming",
                    Brand = "ASUS",
                    Series = "ROG Strix",
                    Price = 29990000m,
                    OldPrice = 32990000m,
                    Discount = 9,
                    Image = "images/products/asus-rog-strix-g16.jpg",
                    Rating = 4.9m,
                    Sold = 128,
                    Stock = 10,
                    Description = "Laptop gaming cao cấp với hiệu năng mạnh mẽ.",
                    Specifications = new Dictionary<string, string>
                    {
                        { "cpu", "Intel Core i7-13650HX" },
                        { "ram", "16GB DDR5" },
                        { "storage", "512GB SSD" },
                        { "gpu", "RTX 4060 8GB" },
                        { "screen", "16 inch 165Hz" }
                    }
                },
                new Product
                {
                    Id = "LG02",
                    Name = "Acer Nitro V15",
                    Category = "laptop-gaming",
                    Brand = "ACER",
                    Series = "Nitro",
                    Price = 18990000m,
                    OldPrice = 20990000m,
                    Discount = 10,
                    Image = "images/products/acer-nitro-v15.jpg",
                    Rating = 4.8m,
                    Sold = 95,
                    Stock = 15,
                    Description = "Laptop gaming phổ thông với hiệu năng tốt.",
                    Specifications = new Dictionary<string, string>
                    {
                        { "cpu", "Intel Core i5-13420H" },
                        { "ram", "16GB DDR5" },
                        { "storage", "512GB SSD" },
                        { "gpu", "RTX 4050 6GB" },
                        { "screen", "15.6 inch 144Hz" }
                    }
                },
                new Product
                {
                    Id = "LG03",
                    Name = "MSI Katana 15",
                    Category = "laptop-gaming",
                    Brand = "MSI",
                    Series = "Katana",
                    Price = 24990000m,
                    OldPrice = 26990000m,
                    Discount = 7,
                    Image = "images/products/msi-katana-15.jpg",
                    Rating = 4.7m,
                    Sold = 76,
                    Stock = 8,
                    Description = "Laptop gaming MSI với cấu hình mạnh.",
                    Specifications = new Dictionary<string, string>
                    {
                        { "cpu", "Intel Core i7-13620H" },
                        { "ram", "16GB DDR5" },
                        { "storage", "1TB SSD" },
                        { "gpu", "RTX 4060 8GB" },
                        { "screen", "15.6 inch 144Hz" }
                    }
                },
                new Product
                {
                    Id = "LG04",
                    Name = "Lenovo LOQ 15",
                    Category = "laptop-gaming",
                    Brand = "LENOVO",
                    Series = "LOQ",
                    Price = 22990000m,
                    OldPrice = 24990000m,
                    Discount = 8,
                    Image = "images/products/lenovo-loq-15.jpg",
                    Rating = 4.8m,
                    Sold = 64,
                    Stock = 14,
                    Description = "Laptop gaming Lenovo cân bằng giữa hiệu năng và giá.",
                    Specifications = new Dictionary<string, string>
                    {
                        { "cpu", "Intel Core i5-13450HX" },
                        { "ram", "16GB DDR5" },
                        { "storage", "512GB SSD" },
                        { "gpu", "RTX 4050 6GB" },
                        { "screen", "15.6 inch 144Hz" }
                    }
                },
                new Product
                {
                    Id = "PC01",
                    Name = "PC Gaming GVN A1",
                    Category = "pc-ps",
                    Brand = "GVN",
                    Series = "Gaming",
                    Price = 25990000m,
                    OldPrice = 27990000m,
                    Discount = 7,
                    Image = "images/products/pc-gvn-a1.jpg",
                    Rating = 4.9m,
                    Sold = 42,
                    Stock = 6,
                    Description = "PC gaming hiệu năng cao cho game thủ.",
                    Specifications = new Dictionary<string, string>
                    {
                        { "cpu", "Intel Core i5-14400F" },
                        { "ram", "16GB DDR5" },
                        { "storage", "1TB SSD" },
                        { "gpu", "RTX 4060 8GB" },
                        { "psu", "650W" }
                    }
                },
                new Product
                {
                    Id = "PC02",
                    Name = "PC Gaming GVN A2",
                    Category = "pc-ps",
                    Brand = "GVN",
                    Series = "Gaming",
                    Price = 32990000m,
                    OldPrice = 35990000m,
                    Discount = 8,
                    Image = "images/products/pc-gvn-a2.jpg",
                    Rating = 4.9m,
                    Sold = 31,
                    Stock = 5,
                    Description = "PC gaming cao cấp cho gaming và streaming.",
                    Specifications = new Dictionary<string, string>
                    {
                        { "cpu", "Intel Core i7-14700F" },
                        { "ram", "32GB DDR5" },
                        { "storage", "1TB SSD" },
                        { "gpu", "RTX 4070" },
                        { "psu", "750W" }
                    }
                },
                new Product
                {
                    Id = "PC03",
                    Name = "PlayStation 5 Slim",
                    Category = "pc-ps",
                    Brand = "Sony",
                    Series = "PlayStation 5",
                    Price = 12990000m,
                    OldPrice = 13990000m,
                    Discount = 7,
                    Image = "images/products/ps5-slim.jpg",
                    Rating = 4.9m,
                    Sold = 67,
                    Stock = 8,
                    Description = "Máy chơi game PlayStation 5 phiên bản Slim.",
                    Specifications = new Dictionary<string, string>
                    {
                        { "storage", "1TB SSD" },
                        { "resolution", "4K" },
                        { "connection", "HDMI / WiFi" },
                        { "controller", "DualSense" }
                    }
                },
                new Product
                {
                    Id = "PC04",
                    Name = "PlayStation 5 Pro",
                    Category = "pc-ps",
                    Brand = "Sony",
                    Series = "PlayStation 5",
                    Price = 18990000m,
                    OldPrice = 19990000m,
                    Discount = 5,
                    Image = "images/products/ps5-pro.jpg",
                    Rating = 4.9m,
                    Sold = 35,
                    Stock = 5,
                    Description = "Console PlayStation cao cấp dành cho gaming 4K.",
                    Specifications = new Dictionary<string, string>
                    {
                        { "storage", "2TB SSD" },
                        { "resolution", "4K" },
                        { "connection", "HDMI / WiFi" },
                        { "controller", "DualSense" }
                    }
                },
                new Product
                {
                    Id = "CP01",
                    Name = "Intel Core i5-14400F",
                    Category = "components",
                    Brand = "Intel",
                    Series = "Core i5",
                    Type = "cpu",
                    Price = 4990000m,
                    OldPrice = 5290000m,
                    Discount = 6,
                    Image = "images/products/intel-i5-14400f.jpg",
                    Rating = 4.9m,
                    Sold = 156,
                    Stock = 20,
                    Description = "CPU Intel dành cho PC gaming.",
                    Specifications = new Dictionary<string, string>
                    {
                        { "socket", "LGA1700" },
                        { "cores", "10 Cores" },
                        { "threads", "16 Threads" },
                        { "boost", "4.7GHz" }
                    }
                },
                new Product
                {
                    Id = "CP02",
                    Name = "ASUS TUF Gaming B760",
                    Category = "components",
                    Brand = "ASUS",
                    Series = "TUF Gaming",
                    Type = "mainboard",
                    Price = 3990000m,
                    OldPrice = 4290000m,
                    Discount = 7,
                    Image = "images/products/asus-b760.jpg",
                    Rating = 4.8m,
                    Sold = 83,
                    Stock = 12,
                    Description = "Mainboard gaming hỗ trợ CPU Intel thế hệ mới.",
                    Specifications = new Dictionary<string, string>
                    {
                        { "socket", "LGA1700" },
                        { "chipset", "B760" },
                        { "ram", "DDR5" },
                        { "formFactor", "ATX" }
                    }
                },
                new Product
                {
                    Id = "CP03",
                    Name = "AMD Ryzen 5 7600",
                    Category = "components",
                    Brand = "AMD",
                    Series = "Ryzen 5",
                    Type = "cpu",
                    Price = 4790000m,
                    OldPrice = 5090000m,
                    Discount = 6,
                    Image = "images/products/ryzen-5-7600.jpg",
                    Rating = 4.8m,
                    Sold = 132,
                    Stock = 18,
                    Description = "CPU AMD hiệu năng cao cho gaming.",
                    Specifications = new Dictionary<string, string>
                    {
                        { "socket", "AM5" },
                        { "cores", "6 Cores" },
                        { "threads", "12 Threads" },
                        { "boost", "5.1GHz" }
                    }
                },
                new Product
                {
                    Id = "CP04",
                    Name = "ASUS Dual RTX 4060 8GB",
                    Category = "components",
                    Brand = "ASUS",
                    Series = "Dual",
                    Type = "vga",
                    Price = 8490000m,
                    OldPrice = 8990000m,
                    Discount = 6,
                    Image = "images/products/asus-rtx-4060.jpg",
                    Rating = 4.9m,
                    Sold = 98,
                    Stock = 9,
                    Description = "Card đồ họa RTX 4060 dành cho gaming Full HD.",
                    Specifications = new Dictionary<string, string>
                    {
                        { "vram", "8GB GDDR6" },
                        { "interface", "PCI Express 4.0" },
                        { "boostClock", "2535MHz" }
                    }
                },
                new Product
                {
                    Id = "CA01",
                    Name = "XIGMATEK Gaming X",
                    Category = "case",
                    Brand = "XIGMATEK",
                    Series = "Gaming",
                    Price = 990000m,
                    OldPrice = 1190000m,
                    Discount = 17,
                    Image = "images/products/xigmatek-gaming-x.jpg",
                    Rating = 4.7m,
                    Sold = 68,
                    Stock = 15,
                    Description = "Case gaming hiện đại với mặt kính cường lực.",
                    Specifications = new Dictionary<string, string>
                    {
                        { "formFactor", "Mid Tower" },
                        { "material", "Steel + Tempered Glass" },
                        { "fan", "4 Fans" }
                    }
                },
                new Product
                {
                    Id = "CA02",
                    Name = "NZXT H5 Flow",
                    Category = "case",
                    Brand = "NZXT",
                    Series = "H5",
                    Price = 2290000m,
                    OldPrice = 2490000m,
                    Discount = 8,
                    Image = "images/products/nzxt-h5-flow.jpg",
                    Rating = 4.9m,
                    Sold = 55,
                    Stock = 8,
                    Description = "Case PC cao cấp với thiết kế tối giản.",
                    Specifications = new Dictionary<string, string>
                    {
                        { "formFactor", "Mid Tower" },
                        { "material", "Steel + Tempered Glass" },
                        { "fan", "2 Fans" }
                    }
                },
                new Product
                {
                    Id = "CA03",
                    Name = "Corsair 4000D Airflow",
                    Category = "case",
                    Brand = "Corsair",
                    Series = "4000D",
                    Price = 2390000m,
                    OldPrice = 2690000m,
                    Discount = 11,
                    Image = "images/products/corsair-4000d.jpg",
                    Rating = 4.9m,
                    Sold = 73,
                    Stock = 10,
                    Description = "Case gaming tối ưu luồng gió.",
                    Specifications = new Dictionary<string, string>
                    {
                        { "formFactor", "Mid Tower" },
                        { "material", "Steel + Tempered Glass" },
                        { "fan", "2 Fans" }
                    }
                },
                new Product
                {
                    Id = "CA04",
                    Name = "Cooler Master TD500 Mesh",
                    Category = "case",
                    Brand = "Cooler Master",
                    Series = "TD500",
                    Price = 1990000m,
                    OldPrice = 2290000m,
                    Discount = 13,
                    Image = "images/products/cooler-master-td500.jpg",
                    Rating = 4.8m,
                    Sold = 49,
                    Stock = 7,
                    Description = "Case gaming hỗ trợ hệ thống tản nhiệt tốt.",
                    Specifications = new Dictionary<string, string>
                    {
                        { "formFactor", "Mid Tower" },
                        { "material", "Steel + Tempered Glass" },
                        { "fan", "3 Fans" }
                    }
                },
                new Product
                {
                    Id = "LM01",
                    Name = "Creative Pebble V3",
                    Category = "speaker-microphone",
                    Brand = "Creative",
                    Series = "Pebble",
                    Type = "speaker",
                    Price = 990000m,
                    OldPrice = 1190000m,
                    Discount = 17,
                    Image = "images/products/creative-pebble-v3.jpg",
                    Rating = 4.7m,
                    Sold = 115,
                    Stock = 20,
                    Description = "Loa máy tính nhỏ gọn dành cho gaming.",
                    Specifications = new Dictionary<string, string>
                    {
                        { "connection", "USB-C / Bluetooth" },
                        { "power", "8W" }
                    }
                },
                new Product
                {
                    Id = "LM02",
                    Name = "Razer Leviathan V2",
                    Category = "speaker-microphone",
                    Brand = "Razer",
                    Series = "Leviathan",
                    Type = "speaker",
                    Price = 5990000m,
                    OldPrice = 6490000m,
                    Discount = 8,
                    Image = "images/products/razer-leviathan-v2.jpg",
                    Rating = 4.9m,
                    Sold = 42,
                    Stock = 6,
                    Description = "Soundbar gaming cao cấp với âm thanh sống động.",
                    Specifications = new Dictionary<string, string>
                    {
                        { "connection", "Bluetooth / USB" },
                        { "channels", "2.1" }
                    }
                },
                new Product
                {
                    Id = "LM03",
                    Name = "HyperX SoloCast",
                    Category = "speaker-microphone",
                    Brand = "HyperX",
                    Series = "SoloCast",
                    Type = "microphone",
                    Price = 1390000m,
                    OldPrice = 1590000m,
                    Discount = 13,
                    Image = "images/products/hyperx-solocast.jpg",
                    Rating = 4.8m,
                    Sold = 89,
                    Stock = 14,
                    Description = "Microphone USB dành cho gaming và streaming.",
                    Specifications = new Dictionary<string, string>
                    {
                        { "type", "Condenser" },
                        { "connection", "USB" },
                        { "pickup", "Cardioid" }
                    }
                },
                new Product
                {
                    Id = "LM04",
                    Name = "FIFINE AM8",
                    Category = "speaker-microphone",
                    Brand = "FIFINE",
                    Series = "AM",
                    Type = "microphone",
                    Price = 1890000m,
                    OldPrice = 2190000m,
                    Discount = 14,
                    Image = "images/products/fifine-am8.jpg",
                    Rating = 4.8m,
                    Sold = 76,
                    Stock = 12,
                    Description = "Microphone gaming dành cho livestream và thu âm.",
                    Specifications = new Dictionary<string, string>
                    {
                        { "type", "Dynamic" },
                        { "connection", "USB / XLR" },
                        { "pickup", "Cardioid" }
                    }
                },
                new Product
                {
                    Id = "MH01",
                    Name = "ASUS TUF Gaming VG249Q3A",
                    Category = "monitor",
                    Brand = "ASUS",
                    Series = "TUF Gaming",
                    Price = 4290000m,
                    OldPrice = 4790000m,
                    Discount = 10,
                    Image = "images/products/asus-vg249q3a.jpg",
                    Rating = 4.8m,
                    Sold = 94,
                    Stock = 11,
                    Description = "Màn hình gaming 180Hz.",
                    Specifications = new Dictionary<string, string>
                    {
                        { "size", "24 inch" },
                        { "resolution", "Full HD" },
                        { "refreshRate", "180Hz" },
                        { "panel", "IPS" }
                    }
                },
                new Product
                {
                    Id = "MH02",
                    Name = "LG UltraGear 27GS60F",
                    Category = "monitor",
                    Brand = "LG",
                    Series = "UltraGear",
                    Price = 5990000m,
                    OldPrice = 6490000m,
                    Discount = 8,
                    Image = "images/products/lg-ultragear.jpg",
                    Rating = 4.9m,
                    Sold = 71,
                    Stock = 8,
                    Description = "Màn hình gaming 27 inch tốc độ cao.",
                    Specifications = new Dictionary<string, string>
                    {
                        { "size", "27 inch" },
                        { "resolution", "Full HD" },
                        { "refreshRate", "180Hz" },
                        { "panel", "IPS" }
                    }
                },
                new Product
                {
                    Id = "MH03",
                    Name = "Samsung Odyssey G5",
                    Category = "monitor",
                    Brand = "Samsung",
                    Series = "Odyssey",
                    Price = 7490000m,
                    OldPrice = 7990000m,
                    Discount = 6,
                    Image = "images/products/samsung-odyssey-g5.jpg",
                    Rating = 4.9m,
                    Sold = 62,
                    Stock = 7,
                    Description = "Màn hình gaming cong với độ phân giải cao.",
                    Specifications = new Dictionary<string, string>
                    {
                        { "size", "27 inch" },
                        { "resolution", "2K" },
                        { "refreshRate", "165Hz" },
                        { "panel", "VA" }
                    }
                },
                new Product
                {
                    Id = "MH04",
                    Name = "MSI G244F",
                    Category = "monitor",
                    Brand = "MSI",
                    Series = "Gaming",
                    Price = 4290000m,
                    OldPrice = 4690000m,
                    Discount = 9,
                    Image = "images/products/msi-g244f.jpg",
                    Rating = 4.8m,
                    Sold = 58,
                    Stock = 9,
                    Description = "Màn hình gaming IPS tốc độ cao.",
                    Specifications = new Dictionary<string, string>
                    {
                        { "size", "24 inch" },
                        { "resolution", "Full HD" },
                        { "refreshRate", "180Hz" },
                        { "panel", "Rapid IPS" }
                    }
                },
                new Product
                {
                    Id = "BP01",
                    Name = "Razer BlackWidow V4",
                    Category = "keyboard",
                    Brand = "Razer",
                    Series = "BlackWidow",
                    Price = 4290000m,
                    OldPrice = 4790000m,
                    Discount = 10,
                    Image = "images/products/razer-blackwidow-v4.jpg",
                    Rating = 4.9m,
                    Sold = 118,
                    Stock = 13,
                    Description = "Bàn phím cơ gaming cao cấp.",
                    Specifications = new Dictionary<string, string>
                    {
                        { "type", "Mechanical" },
                        { "switch", "Green Switch" },
                        { "layout", "Fullsize" },
                        { "connection", "USB" }
                    }
                },
                new Product
                {
                    Id = "BP02",
                    Name = "Logitech G Pro X TKL",
                    Category = "keyboard",
                    Brand = "Logitech",
                    Series = "G Pro",
                    Price = 3990000m,
                    OldPrice = 4490000m,
                    Discount = 11,
                    Image = "images/products/logitech-g-pro-x-tkl.jpg",
                    Rating = 4.8m,
                    Sold = 87,
                    Stock = 10,
                    Description = "Bàn phím gaming TKL chuyên nghiệp.",
                    Specifications = new Dictionary<string, string>
                    {
                        { "type", "Mechanical" },
                        { "switch", "GX" },
                        { "layout", "TKL" },
                        { "connection", "Wireless" }
                    }
                },
                new Product
                {
                    Id = "BP03",
                    Name = "Akko 5075B Plus",
                    Category = "keyboard",
                    Brand = "Akko",
                    Series = "5075",
                    Price = 1990000m,
                    OldPrice = 2290000m,
                    Discount = 13,
                    Image = "images/products/akko-5075b.jpg",
                    Rating = 4.8m,
                    Sold = 143,
                    Stock = 18,
                    Description = "Bàn phím cơ layout 75% dành cho gaming.",
                    Specifications = new Dictionary<string, string>
                    {
                        { "type", "Mechanical" },
                        { "switch", "Akko" },
                        { "layout", "75%" },
                        { "connection", "Wireless" }
                    }
                },
                new Product
                {
                    Id = "BP04",
                    Name = "Corsair K70 RGB",
                    Category = "keyboard",
                    Brand = "Corsair",
                    Series = "K70",
                    Price = 3490000m,
                    OldPrice = 3990000m,
                    Discount = 13,
                    Image = "images/products/corsair-k70.jpg",
                    Rating = 4.8m,
                    Sold = 65,
                    Stock = 9,
                    Description = "Bàn phím cơ gaming với hệ thống RGB.",
                    Specifications = new Dictionary<string, string>
                    {
                        { "type", "Mechanical" },
                        { "switch", "Red Switch" },
                        { "layout", "Fullsize" },
                        { "connection", "USB" }
                    }
                },
                new Product
                {
                    Id = "CH01",
                    Name = "Logitech G502 X",
                    Category = "mouse-mousepad",
                    Brand = "Logitech",
                    Series = "G502",
                    Type = "mouse",
                    Price = 2990000m,
                    OldPrice = 3290000m,
                    Discount = 9,
                    Image = "images/products/logitech-g502-x.jpg",
                    Rating = 4.9m,
                    Sold = 235,
                    Stock = 25,
                    Description = "Chuột gaming với nhiều nút tùy chỉnh.",
                    Specifications = new Dictionary<string, string>
                    {
                        { "sensor", "HERO 25K" },
                        { "dpi", "25600 DPI" },
                        { "connection", "Wired" },
                        { "buttons", "13" }
                    }
                },
                new Product
                {
                    Id = "CH02",
                    Name = "Razer DeathAdder V3",
                    Category = "mouse-mousepad",
                    Brand = "Razer",
                    Series = "DeathAdder",
                    Type = "mouse",
                    Price = 2490000m,
                    OldPrice = 2790000m,
                    Discount = 11,
                    Image = "images/products/razer-deathadder-v3.jpg",
                    Rating = 4.9m,
                    Sold = 174,
                    Stock = 18,
                    Description = "Chuột gaming công thái học nhẹ.",
                    Specifications = new Dictionary<string, string>
                    {
                        { "sensor", "Focus Pro 30K" },
                        { "dpi", "30000 DPI" },
                        { "connection", "Wired" },
                        { "weight", "63g" }
                    }
                },
                new Product
                {
                    Id = "CH03",
                    Name = "Logitech G640 Mouse Pad",
                    Category = "mouse-mousepad",
                    Brand = "Logitech",
                    Series = "G640",
                    Type = "mousepad",
                    Price = 690000m,
                    OldPrice = 790000m,
                    Discount = 13,
                    Image = "images/products/logitech-g640.jpg",
                    Rating = 4.8m,
                    Sold = 210,
                    Stock = 30,
                    Description = "Lót chuột gaming kích thước lớn.",
                    Specifications = new Dictionary<string, string>
                    {
                        { "size", "400 x 460mm" },
                        { "surface", "Soft Cloth" },
                        { "thickness", "3mm" }
                    }
                },
                new Product
                {
                    Id = "CH04",
                    Name = "Razer Gigantus V2",
                    Category = "mouse-mousepad",
                    Brand = "Razer",
                    Series = "Gigantus",
                    Type = "mousepad",
                    Price = 590000m,
                    OldPrice = 690000m,
                    Discount = 14,
                    Image = "images/products/razer-gigantus-v2.jpg",
                    Rating = 4.8m,
                    Sold = 156,
                    Stock = 22,
                    Description = "Lót chuột gaming bề mặt vải mịn.",
                    Specifications = new Dictionary<string, string>
                    {
                        { "size", "450 x 400mm" },
                        { "surface", "Cloth" },
                        { "thickness", "3mm" }
                    }
                },
                new Product
                {
                    Id = "TN01",
                    Name = "HyperX Cloud III",
                    Category = "headphone",
                    Brand = "HyperX",
                    Series = "Cloud",
                    Price = 2490000m,
                    OldPrice = 2790000m,
                    Discount = 11,
                    Image = "images/products/hyperx-cloud-3.jpg",
                    Rating = 4.9m,
                    Sold = 198,
                    Stock = 20,
                    Description = "Tai nghe gaming với âm thanh rõ ràng.",
                    Specifications = new Dictionary<string, string>
                    {
                        { "driver", "53mm" },
                        { "connection", "Wired" },
                        { "microphone", "Noise Cancelling" },
                        { "surround", "DTS Headphone:X" }
                    }
                },
                new Product
                {
                    Id = "TN02",
                    Name = "Razer BlackShark V2",
                    Category = "headphone",
                    Brand = "Razer",
                    Series = "BlackShark",
                    Price = 2190000m,
                    OldPrice = 2490000m,
                    Discount = 12,
                    Image = "images/products/razer-blackshark-v2.jpg",
                    Rating = 4.8m,
                    Sold = 143,
                    Stock = 17,
                    Description = "Tai nghe gaming nhẹ với microphone rõ tiếng.",
                    Specifications = new Dictionary<string, string>
                    {
                        { "driver", "50mm" },
                        { "connection", "USB" },
                        { "microphone", "HyperClear" },
                        { "surround", "7.1" }
                    }
                },
                new Product
                {
                    Id = "TN03",
                    Name = "Logitech G Pro X 2",
                    Category = "headphone",
                    Brand = "Logitech",
                    Series = "G Pro X",
                    Price = 4990000m,
                    OldPrice = 5490000m,
                    Discount = 9,
                    Image = "images/products/logitech-g-pro-x2.jpg",
                    Rating = 4.9m,
                    Sold = 84,
                    Stock = 10,
                    Description = "Tai nghe gaming không dây cao cấp.",
                    Specifications = new Dictionary<string, string>
                    {
                        { "driver", "50mm" },
                        { "connection", "Wireless" },
                        { "microphone", "Detachable" },
                        { "surround", "DTS" }
                    }
                },
                new Product
                {
                    Id = "TN04",
                    Name = "SteelSeries Arctis Nova 7",
                    Category = "headphone",
                    Brand = "SteelSeries",
                    Series = "Arctis Nova",
                    Price = 3990000m,
                    OldPrice = 4490000m,
                    Discount = 11,
                    Image = "images/products/arctis-nova-7.jpg",
                    Rating = 4.9m,
                    Sold = 73,
                    Stock = 8,
                    Description = "Tai nghe gaming không dây đa nền tảng.",
                    Specifications = new Dictionary<string, string>
                    {
                        { "driver", "40mm" },
                        { "connection", "Wireless / Bluetooth" },
                        { "microphone", "Retractable" },
                        { "surround", "360 Spatial Audio" }
                    }
                },
                new Product
                {
                    Id = "GB01",
                    Name = "Ghế Gaming GVN Basic",
                    Category = "chair-desk",
                    Brand = "GVN",
                    Series = "Basic",
                    Type = "chair",
                    Price = 2490000m,
                    OldPrice = 2990000m,
                    Discount = 17,
                    Image = "images/products/gvn-gaming-chair.jpg",
                    Rating = 4.7m,
                    Sold = 54,
                    Stock = 9,
                    Description = "Ghế gaming hỗ trợ tư thế ngồi thoải mái.",
                    Specifications = new Dictionary<string, string>
                    {
                        { "material", "PU Leather" },
                        { "recline", "90 - 135 độ" },
                        { "load", "120kg" }
                    }
                },
                new Product
                {
                    Id = "GB02",
                    Name = "Anda Seat Dark Demon",
                    Category = "chair-desk",
                    Brand = "Anda Seat",
                    Series = "Dark Demon",
                    Type = "chair",
                    Price = 6990000m,
                    OldPrice = 7490000m,
                    Discount = 7,
                    Image = "images/products/anda-seat-dark-demon.jpg",
                    Rating = 4.9m,
                    Sold = 42,
                    Stock = 6,
                    Description = "Ghế gaming cao cấp với thiết kế công thái học.",
                    Specifications = new Dictionary<string, string>
                    {
                        { "material", "PVC Leather" },
                        { "recline", "90 - 160 độ" },
                        { "load", "150kg" }
                    }
                },
                new Product
                {
                    Id = "GB03",
                    Name = "Bàn Gaming E-DRA LUX",
                    Category = "chair-desk",
                    Brand = "E-DRA",
                    Series = "Gaming",
                    Type = "desk",
                    Price = 2490000m,
                    OldPrice = 2790000m,
                    Discount = 11,
                    Image = "images/products/edra-gaming-desk.jpg",
                    Rating = 4.8m,
                    Sold = 63,
                    Stock = 10,
                    Description = "Bàn gaming rộng rãi với thiết kế hiện đại.",
                    Specifications = new Dictionary<string, string>
                    {
                        { "width", "120cm" },
                        { "material", "MDF" },
                        { "height", "75cm" }
                    }
                },
                new Product
                {
                    Id = "GB04",
                    Name = "Bàn nâng hạ FlexiSpot",
                    Category = "chair-desk",
                    Brand = "FlexiSpot",
                    Series = "Standing Desk",
                    Type = "desk",
                    Price = 5990000m,
                    OldPrice = 6490000m,
                    Discount = 8,
                    Image = "images/products/flexispot-desk.jpg",
                    Rating = 4.9m,
                    Sold = 38,
                    Stock = 5,
                    Description = "Bàn nâng hạ điện phù hợp gaming và làm việc.",
                    Specifications = new Dictionary<string, string>
                    {
                        { "width", "140cm" },
                        { "material", "MDF" },
                        { "height", "72 - 121cm" }
                    }
                }
            };
        }
    }
}
