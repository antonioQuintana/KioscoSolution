USE KioscoDB;
GO

-- ==============================================================================
-- 1. CREACIÓN DE CATEGORÍAS (Sólo se insertan si no existen)
-- ==============================================================================
IF NOT EXISTS (SELECT 1 FROM Categorias WHERE LOWER(Nombre) = 'bebidas sin alcohol')
    INSERT INTO Categorias (Nombre, Descripcion) VALUES ('Bebidas sin alcohol', 'Gaseosas, aguas, jugos e isotónicas');

IF NOT EXISTS (SELECT 1 FROM Categorias WHERE LOWER(Nombre) = 'cervezas')
    INSERT INTO Categorias (Nombre, Descripcion) VALUES ('Cervezas', 'Cervezas en lata, botella y aperitivos en lata');

IF NOT EXISTS (SELECT 1 FROM Categorias WHERE LOWER(Nombre) = 'golosinas')
    INSERT INTO Categorias (Nombre, Descripcion) VALUES ('Golosinas', 'Alfajores, chocolates, caramelos, galletitas y dulces');

IF NOT EXISTS (SELECT 1 FROM Categorias WHERE LOWER(Nombre) = 'cigarrillos')
    INSERT INTO Categorias (Nombre, Descripcion) VALUES ('Cigarrillos', 'Atados, tabaco y encendedores');

IF NOT EXISTS (SELECT 1 FROM Categorias WHERE LOWER(Nombre) = 'snacks')
    INSERT INTO Categorias (Nombre, Descripcion) VALUES ('Snacks', 'Papas fritas, palitos salados y maníes');

IF NOT EXISTS (SELECT 1 FROM Categorias WHERE LOWER(Nombre) = 'varios')
    INSERT INTO Categorias (Nombre, Descripcion) VALUES ('Varios', 'Pilas, accesorios y artículos de kiosco general');
GO

-- ==============================================================================
-- 2. INSERTAR PRODUCTOS
-- ==============================================================================

-- ------------------------------------------------------------------------------
-- CIGARRILLOS
-- ------------------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790001001')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790001001', 'Marlboro Box 20', 'Cigarrillos Marlboro Box común', (SELECT TOP 1 Id FROM Categorias WHERE Nombre = 'Cigarrillos'), 2200.00, 2600.00, 40, 10);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790001002')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo) -- Alternativa directa
    VALUES ('7790001002', 'Philip Morris Box 20', 'Cigarrillos Philip Morris Box', (SELECT TOP 1 Id FROM Categorias WHERE Nombre = 'Cigarrillos'), 2000.00, 2400.00, 35, 10);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790001003')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790001003', 'Lucky Strike Box 20', 'Cigarrillos Lucky Strike Convert', (SELECT TOP 1 Id FROM Categorias WHERE Nombre = 'Cigarrillos'), 1900.00, 2300.00, 30, 10);

-- ------------------------------------------------------------------------------
-- GASEOSAS INDIVIDUALES
-- ------------------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790002001')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790002001', 'Coca-Cola 500ml', 'Gaseosa Coca-Cola botella PET', (SELECT TOP 1 Id FROM Categorias WHERE Nombre = 'Bebidas sin alcohol'), 900.00, 1500.00, 60, 15);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790002002')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790002002', 'Pepsi 500ml', 'Gaseosa Pepsi botella PET', (SELECT TOP 1 Id FROM Categorias WHERE Nombre = 'Bebidas sin alcohol'), 800.00, 1300.00, 40, 10);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790002003')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790002003', 'Coca-Cola 1.5L', 'Gaseosa Coca-Cola 1.5 litros', (SELECT TOP 1 Id FROM Categorias WHERE Nombre = 'Bebidas sin alcohol'), 1600.00, 2400.00, 30, 8);

-- ------------------------------------------------------------------------------
-- ALFAJORES CLÁSICOS
-- ------------------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790003001')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790003001', 'Alfajor Guaymallén Chocolate', 'Alfajor simple relleno con dulce de leche', (SELECT TOP 1 Id FROM Categorias WHERE Nombre = 'Golosinas'), 200.00, 400.00, 120, 24);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790003002')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790003002', 'Alfajor Fantoche Triple', 'Alfajor triple blanco y negro', (SELECT TOP 1 Id FROM Categorias WHERE Nombre = 'Golosinas'), 500.00, 900.00, 60, 15);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790003003')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790003003', 'Alfajor Terrabusi Torta', 'Alfajor relleno con dulce de leche bañado en chocolate', (SELECT TOP 1 Id FROM Categorias WHERE Nombre = 'Golosinas'), 600.00, 1100.00, 50, 15);

-- ------------------------------------------------------------------------------
-- CERVEZAS EN LATA (473 ml)
-- ------------------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790004001')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790004001', 'Cerveza Quilmes Clásica 473ml', 'Lata de cerveza rubia', (SELECT TOP 1 Id FROM Categorias WHERE Nombre = 'Cervezas'), 1000.00, 1600.00, 72, 18);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790004002')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790004002', 'Cerveza Brahma 473ml', 'Lata de cerveza rubia suave', (SELECT TOP 1 Id FROM Categorias WHERE Nombre = 'Cervezas'), 950.00, 1550.00, 60, 18);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790004003')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790004003', 'Cerveza Heineken 473ml', 'Lata de cerveza premium', (SELECT TOP 1 Id FROM Categorias WHERE Nombre = 'Cervezas'), 1400.00, 2200.00, 48, 12);

-- ------------------------------------------------------------------------------
-- AGUAS Y AGUAS SABORIZADAS
-- ------------------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790005001')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790005001', 'Agua Villavicencio Sin Gas 500ml', 'Agua mineral natural', (SELECT TOP 1 Id FROM Categorias WHERE Nombre = 'Bebidas sin alcohol'), 550.00, 950.00, 50, 15);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790005002')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790005002', 'Agua Saborizada Ser Manzana 500ml', 'Agua finamente gasificada sabor manzana', (SELECT TOP 1 Id FROM Categorias WHERE Nombre = 'Bebidas sin alcohol'), 650.00, 1100.00, 40, 10);

-- ------------------------------------------------------------------------------
-- CHUPETINES, TURRONES Y BARRITAS
-- ------------------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790006001')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790006001', 'Chupetín Pico Dulce', 'Chupetín tuti-frutti clásico', (SELECT TOP 1 Id FROM Categorias WHERE Nombre = 'Golosinas'), 120.00, 250.00, 150, 30);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790007001')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790007001', 'Turrón de Maní Arcor 25g', 'Turrón oblea relleno con pasta de maní', (SELECT TOP 1 Id FROM Categorias WHERE Nombre = 'Golosinas'), 150.00, 300.00, 120, 25);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790008001')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790008001', 'Barrita Vauquita 50g', 'Tableta tradicional de dulce de leche', (SELECT TOP 1 Id FROM Categorias WHERE Nombre = 'Golosinas'), 400.00, 750.00, 80, 20);

-- ------------------------------------------------------------------------------
-- CARAMELOS, PASTILLAS Y CHICLES
-- ------------------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790009001')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790009001', 'Caramelos Palitos de la Selva 660g', 'Bolsa de caramelos masticables', (SELECT TOP 1 Id FROM Categorias WHERE Nombre = 'Golosinas'), 2800.00, 4200.00, 15, 5);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790009002')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790009002', 'Caramelos Sugus Confites 50g', 'Cajita de confites masticables', (SELECT TOP 1 Id FROM Categorias WHERE Nombre = 'Golosinas'), 450.00, 800.00, 60, 15);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790009003')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790009003', 'Pastillas Halls Mentol Extra Fuerte', 'Tubo de pastillas refrescantes', (SELECT TOP 1 Id FROM Categorias WHERE Nombre = 'Golosinas'), 350.00, 650.00, 75, 20);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790010001')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790010001', 'Chicles Beldent Menta', 'Cajita de chicles sin azúcar', (SELECT TOP 1 Id FROM Categorias WHERE Nombre = 'Golosinas'), 400.00, 750.00, 90, 25);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790010002')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790010002', 'Chicles Bubbaloo Frutilla', 'Chicle globo con centro líquido', (SELECT TOP 1 Id FROM Categorias WHERE Nombre = 'Golosinas'), 150.00, 300.00, 100, 30);

-- ------------------------------------------------------------------------------
-- CHOCOLATES, BOMBONES Y ALFAJORES PREMIUM
-- ------------------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790011001')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790011001', 'Chocolate Cofler Block 38g', 'Barra de chocolate con leche y maní', (SELECT TOP 1 Id FROM Categorias WHERE Nombre = 'Golosinas'), 550.00, 1000.00, 70, 20);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790011002')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790011002', 'Bombón Bon o Bon Clásico', 'Bombón relleno con crema de maní', (SELECT TOP 1 Id FROM Categorias WHERE Nombre = 'Golosinas'), 220.00, 450.00, 100, 25);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790011003')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790011003', 'Chocolate Dos Corazones Felfort', 'Bombón de chocolate relleno con poema', (SELECT TOP 1 Id FROM Categorias WHERE Nombre = 'Golosinas'), 350.00, 650.00, 80, 20);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790011004')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790011004', 'Bocadito Cabsha', 'Bocadito de dulce de leche bañado en chocolate', (SELECT TOP 1 Id FROM Categorias WHERE Nombre = 'Golosinas'), 220.00, 400.00, 90, 20);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790012001')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790012001', 'Alfajor Cachafaz Chocolate', 'Alfajor de autor bañado en chocolate negro', (SELECT TOP 1 Id FROM Categorias WHERE Nombre = 'Golosinas'), 950.00, 1600.00, 50, 15);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790012002')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790012002', 'Alfajor Rasta Blanco', 'Alfajor con galleta crocante y dulce de leche', (SELECT TOP 1 Id FROM Categorias WHERE Nombre = 'Golosinas'), 900.00, 1500.00, 50, 15);

-- ------------------------------------------------------------------------------
-- GALLETITAS INDIVIDUALES
-- ------------------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790013001')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790013001', 'Galletitas Oreo 118g', 'Paquete individual de galletitas rellenas', (SELECT TOP 1 Id FROM Categorias WHERE Nombre = 'Golosinas'), 700.00, 1200.00, 45, 12);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790013002')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790013002', 'Galletitas Pepitos 119g', 'Galletitas con chispas de chocolate', (SELECT TOP 1 Id FROM Categorias WHERE Nombre = 'Golosinas'), 680.00, 1150.00, 40, 12);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790013003')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790013003', 'Galletitas Pitusas Vainilla 160g', 'Paquete de galletitas rellenas sabor vainilla', (SELECT TOP 1 Id FROM Categorias WHERE Nombre = 'Golosinas'), 450.00, 800.00, 50, 15);

-- ------------------------------------------------------------------------------
-- SNACKS SALADOS
-- ------------------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790014001')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790014001', 'Palitos Saladix Pizza 100g', 'Snack horneado sabor pizza', (SELECT TOP 1 Id FROM Categorias WHERE Nombre = 'Snacks'), 650.00, 1100.00, 40, 10);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790014002')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790014002', 'Papas Fritas Pehuamar 140g', 'Papas fritas clásicas en bolsa', (SELECT TOP 1 Id FROM Categorias WHERE Nombre = 'Snacks'), 1100.00, 1800.00, 30, 8);

-- ------------------------------------------------------------------------------
-- JUGOS, ISOTÓNICAS Y ENERGIZANTES
-- ------------------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790015001')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790015001', 'Jugo Baggio Pronto Naranja 200ml', 'Jugo en cajita individual con sorbete', (SELECT TOP 1 Id FROM Categorias WHERE Nombre = 'Bebidas sin alcohol'), 300.00, 550.00, 60, 15);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790015002')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790015002', 'Bebida Isotónica Gatorade 500ml', 'Bebida isotónica sabor manzana / cool blue', (SELECT TOP 1 Id FROM Categorias WHERE Nombre = 'Bebidas sin alcohol'), 950.00, 1600.00, 40, 10);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790015003')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790015003', 'Bebida Isotónica Powerade 500ml', 'Bebida para deportistas sabor mountain blast', (SELECT TOP 1 Id FROM Categorias WHERE Nombre = 'Bebidas sin alcohol'), 900.00, 1550.00, 40, 10);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790016001')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790016001', 'Energizante Speed Unlimited 250ml', 'Lata de bebida energizante', (SELECT TOP 1 Id FROM Categorias WHERE Nombre = 'Bebidas sin alcohol'), 850.00, 1500.00, 60, 15);

-- ------------------------------------------------------------------------------
-- VINOS Y APERITIVOS EN FORMATO CHICO
-- ------------------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790017001')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790017001', 'Fernet Branca con Cola Lata 473ml', 'Trago listo para tomar en lata', (SELECT TOP 1 Id FROM Categorias WHERE Nombre = 'Cervezas'), 1800.00, 2800.00, 36, 10);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790017002')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790017002', 'Gancia One Lata 473ml', 'Aperitivo listo para tomar en lata', (SELECT TOP 1 Id FROM Categorias WHERE Nombre = 'Cervezas'), 1400.00, 2200.00, 36, 10);

-- ------------------------------------------------------------------------------
-- ENCENDEDORES Y PILAS
-- ------------------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790018001')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790018001', 'Encendedor BIC Maxi', 'Encendedor a gas descartable grande', (SELECT TOP 1 Id FROM Categorias WHERE Nombre = 'Cigarrillos'), 800.00, 1400.00, 50, 15);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790019001')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790019001', 'Pilas Duracell AA x2', 'Blíster de 2 pilas alcalinas AA', (SELECT TOP 1 Id FROM Categorias WHERE Nombre = 'Varios'), 1600.00, 2600.00, 25, 8);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790019002')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790019002', 'Pilas Energizer AAA x2', 'Blíster de 2 pilas alcalinas AAA', (SELECT TOP 1 Id FROM Categorias WHERE Nombre = 'Varios'), 1500.00, 2500.00, 25, 8);
GO