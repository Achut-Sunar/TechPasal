-- TechPasal Database Schema

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Users' AND xtype='U')
CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(100) NOT NULL,
    Email NVARCHAR(200) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(500) NOT NULL,
    Role NVARCHAR(50) NOT NULL DEFAULT 'Customer',
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
);

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Products' AND xtype='U')
CREATE TABLE Products (
    ProductId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX),
    Price DECIMAL(18,2) NOT NULL,
    DiscountedPrice DECIMAL(18,2) NULL,
    Stock INT NOT NULL DEFAULT 0,
    Category NVARCHAR(100),
    ImageUrl NVARCHAR(500),
    IsFeatured BIT NOT NULL DEFAULT 0,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
);

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Orders' AND xtype='U')
CREATE TABLE Orders (
    OrderId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL REFERENCES Users(UserId),
    Status NVARCHAR(50) NOT NULL DEFAULT 'Pending',
    TotalAmount DECIMAL(18,2) NOT NULL,
    PaymentMethod NVARCHAR(50) NOT NULL,
    PaymentStatus NVARCHAR(50) NOT NULL DEFAULT 'Pending',
    ShippingAddress NVARCHAR(500),
    CouponCode NVARCHAR(50) NULL,
    DiscountAmount DECIMAL(18,2) NOT NULL DEFAULT 0,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
);

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='OrderDetails' AND xtype='U')
CREATE TABLE OrderDetails (
    OrderDetailId INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT NOT NULL REFERENCES Orders(OrderId),
    ProductId INT NOT NULL,
    ProductName NVARCHAR(200) NOT NULL,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(18,2) NOT NULL,
    Subtotal DECIMAL(18,2) NOT NULL
);

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Coupons' AND xtype='U')
CREATE TABLE Coupons (
    CouponId INT IDENTITY(1,1) PRIMARY KEY,
    Code NVARCHAR(50) NOT NULL UNIQUE,
    DiscountPercent DECIMAL(5,2) NOT NULL,
    MaxUses INT NOT NULL DEFAULT 100,
    UsedCount INT NOT NULL DEFAULT 0,
    ExpiresAt DATETIME NULL,
    IsActive BIT NOT NULL DEFAULT 1
);

-- Seed admin user
-- NOTE: The placeholder hash below is intentionally invalid. Run the application's
-- Register endpoint or use UserRepository.HashPassword("Admin@123") to generate a
-- valid PBKDF2 hash and UPDATE this row before first login.
IF NOT EXISTS (SELECT 1 FROM Users WHERE Email='admin@techpasal.com')
INSERT INTO Users (Username, Email, PasswordHash, Role) VALUES 
('Admin', 'admin@techpasal.com', 'PBKDF2$10000$PLACEHOLDER$RESET_REQUIRED', 'Admin');

-- Seed sample products
IF NOT EXISTS (SELECT 1 FROM Products)
INSERT INTO Products (Name, Description, Price, DiscountedPrice, Stock, Category, ImageUrl, IsFeatured) VALUES
('Laptop Pro X1', 'High performance laptop with Intel Core i7', 85000, 79000, 15, 'Laptops', '/Content/images/laptop.jpg', 1),
('Wireless Mouse', 'Ergonomic wireless mouse with 2.4GHz receiver', 1500, NULL, 50, 'Accessories', '/Content/images/mouse.jpg', 1),
('Mechanical Keyboard', 'RGB mechanical keyboard with Cherry MX switches', 6500, 5999, 20, 'Accessories', '/Content/images/keyboard.jpg', 1),
('Monitor 24"', '24 inch Full HD IPS monitor', 22000, 19500, 10, 'Monitors', '/Content/images/monitor.jpg', 1),
('USB-C Hub', '7-in-1 USB-C hub with HDMI, USB 3.0 ports', 3500, NULL, 30, 'Accessories', '/Content/images/hub.jpg', 0),
('SSD 1TB', 'NVMe SSD 1TB high speed storage', 12000, 10500, 25, 'Storage', '/Content/images/ssd.jpg', 1);

-- Seed sample coupon
IF NOT EXISTS (SELECT 1 FROM Coupons WHERE Code='WELCOME10')
INSERT INTO Coupons (Code, DiscountPercent, MaxUses, UsedCount, IsActive) VALUES ('WELCOME10', 10, 1000, 0, 1);
