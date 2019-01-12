
Opomba: To ni dejanska skripta, ki ustvari podatkovno bazo to je samo datoteka s katero sem si pomagal, da sem ustvaril podatkovno
bazo. Celotno podatkovno bazo sem ustvaril v oblaku Azure.


CREATE TABLE Poslovalnica(
ID_poslovalnice integer not null IDENTITY(1,1),
Naziv varchar(50) not null,
Naslov varchar(50) not null,
primary key(ID_poslovalnice));

CREATE TABLE Uporabnik(
ID_uporabnika integer not null IDENTITY(1,1),
Uporabnisko_ime varchar(50) not null,
geslo varchar(50) not null,
primary key(ID_uporabnika));

CREATE TABLE Stranka(
ID_stranke integer not null IDENTITY(1,1),
Ime varchar(15) not null,
Priimek varchar(30) not null,
email varchar(50) not null,
telefon varchar(9) not null,
ID_uporabnika integer not null,
primary key(ID_stranke),
foreign key(ID_uporabnika) REFERENCES Uporabnik(ID_uporabnika));

CREATE TABLE Zaposleni(
ID_zaposleni integer not null IDENTITY(1,1),
Ime varchar(15) not null,
Priimek varchar(20) not null,
ID_poslovalnice integer not null,
ID_uporabnika integer not null,
primary key(ID_zaposleni),
foreign key(ID_poslovalnice) REFERENCES Poslovalnica(ID_poslovalnice),
foreign key(ID_uporabnika) REFERENCES Uporabnik(ID_uporabnika));

CREATE TABLE Narocilo(
ID_narocila integer not null IDENTITY(1,1),
ID_stranke integer not null,
ID_poslovalnice integer not null,
ID_vozila integer not null,
ID_znamke integer not null,
ID_modela integer not null,
ura integer not null,
minuta integer not null,
dan integer not null,
mesec integer not null,
leto integer not null,
potrjen bolean,
opis varchar(500) not null,
primary key(ID_narocila,ID_stranke,ID_poslovalnice,ID_vozila,ID_znamke,ID_modela),
foreign key(ID_stranke) REFERENCES Stranka(ID_stranke) ON DELETE CASCADE,
foreign key(ID_poslovalnice) REFERENCES Poslovalnica(ID_poslovalnice) ON DELETE CASCADE,
foreign key(ID_vozila,ID_znamke,ID_modela) REFERENCES Vozilo(ID_vozila,ID_znamke,ID_modela) ON DELETE CASCADE);


CREATE TABLE Znamka(
ID_znamke integer not null IDENTITY(1,1),
Naziv_znamke varchar(50) not null,
primary key(ID_znamke));


CREATE TABLE Model(
ID_modela integer not null IDENTITY(1,1),
Naziv_modela varchar(50) not null,
ID_znamke integer not null,
primary key(ID_modela,ID_znamke),
foreign key(ID_znamke) REFERENCES Znamka(ID_znamke));


CREATE TABLE Vozilo(
ID_vozila integer not null IDENTITY(1,1),
Letnica integer not null,
ID_znamke integer not null,
ID_modela integer not null,
ID_stranke integer not null,
primary key(ID_vozila,ID_znamke,ID_modela),
foreign key(ID_modela,ID_znamke) REFERENCES Model(ID_modela,ID_znamke),
foreign key(ID_stranke) REFERENCES Stranka(ID_stranke));

Server=tcp:servisvozil.database.windows.net,1433;Initial Catalog=servisvozil;Persist Security Info=False;User ID={your_username};Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;

INSERT INTO Znamka VALUES('Audi');
INSERT INTO Znamka VALUES('BMW');
INSERT INTO Znamka VALUES('Ford');
INSERT INTO Znamka VALUES('Opel');
INSERT INTO Znamka VALUES('Peugeot');
INSERT INTO Znamka VALUES('Citroen');
INSERT INTO Znamka VALUES('Mercedes');
INSERT INTO Znamka VALUES('Volkswagen');
INSERT INTO Znamka VALUES('Suzuki');
INSERT INTO Znamka VALUES('Subaru');
INSERT INTO Znamka VALUES('Michubishi');
INSERT INTO Znamka VALUES('Hyundai');
INSERT INTO Znamka VALUES('Ferrari');
INSERT INTO Znamka VALUES('Lamborghini');
INSERT INTO Znamka VALUES('Porsche');
INSERT INTO Znamka VALUES('Seat');
INSERT INTO Znamka VALUES('Nissan');
INSERT INTO Znamka VALUES('Skoda');
INSERT INTO Znamka VALUES('Renault');
INSERT INTO Znamka VALUES('Alfa Romeo');
INSERT INTO Znamka VALUES('Bentley');
INSERT INTO Znamka VALUES('Bugatti');
INSERT INTO Znamka VALUES('Honda');
INSERT INTO Znamka VALUES('Fiat');
INSERT INTO Znamka VALUES('Jaguar');
INSERT INTO Znamka VALUES('Jeep');
INSERT INTO Znamka VALUES('Kia');
INSERT INTO Znamka VALUES('Land rover');
INSERT INTO Znamka VALUES('Lancia');
INSERT INTO Znamka VALUES('Audi');
INSERT INTO Znamka VALUES('Mazda');
INSERT INTO Znamka VALUES('Saab');
INSERT INTO Znamka VALUES('Toyota');
INSERT INTO Znamka VALUES('Volvo');
INSERT INTO Znamka VALUES('Tesla');

INSERT INTO Model VALUES('A1',1);
INSERT INTO Model VALUES('A2',1);
INSERT INTO Model VALUES('A3',1);
INSERT INTO Model VALUES('A4',1);
INSERT INTO Model VALUES('A5',1);
INSERT INTO Model VALUES('A6',1);
INSERT INTO Model VALUES('A7',1);
INSERT INTO Model VALUES('A8',1);
INSERT INTO Model VALUES('Q1',1);
INSERT INTO Model VALUES('Q2',1);
INSERT INTO Model VALUES('Q3',1);
INSERT INTO Model VALUES('Q5',1);
INSERT INTO Model VALUES('Q7',1);
INSERT INTO Model VALUES('RS2',1);
INSERT INTO Model VALUES('RS3',1);
INSERT INTO Model VALUES('RS4',1);
INSERT INTO Model VALUES('RS5',1);
INSERT INTO Model VALUES('RS6',1);
INSERT INTO Model VALUES('RS7',1);
INSERT INTO Model VALUES('R8',1);
INSERT INTO Model VALUES('S1',1);
INSERT INTO Model VALUES('S2',1);
INSERT INTO Model VALUES('S3',1);
INSERT INTO Model VALUES('S4',1);
INSERT INTO Model VALUES('S5',1);
INSERT INTO Model VALUES('S6',1);
INSERT INTO Model VALUES('S7',1);
INSERT INTO Model VALUES('S8',1);

INSERT INTO Model VALUES('i3',2);
INSERT INTO Model VALUES('i8',2);
INSERT INTO Model VALUES('M1',2);
INSERT INTO Model VALUES('M2',2);
INSERT INTO Model VALUES('M3',2);
INSERT INTO Model VALUES('M4',2);
INSERT INTO Model VALUES('M5',2);
INSERT INTO Model VALUES('M6',2);
INSERT INTO Model VALUES('Serija 1',2);
INSERT INTO Model VALUES('Serija 2',2);
INSERT INTO Model VALUES('Serija 3',2);
INSERT INTO Model VALUES('Serija 4',2);
INSERT INTO Model VALUES('Serija 5',2);
INSERT INTO Model VALUES('Serija 6',2);
INSERT INTO Model VALUES('Serija 7',2);
INSERT INTO Model VALUES('Serija 8',2);
INSERT INTO Model VALUES('Serija X1',2);
INSERT INTO Model VALUES('Serija X2',2);
INSERT INTO Model VALUES('Serija X3',2);
INSERT INTO Model VALUES('Serija X4',2);
INSERT INTO Model VALUES('Serija X5',2);
INSERT INTO Model VALUES('Serija X6',2);
INSERT INTO Model VALUES('Z1',2);
INSERT INTO Model VALUES('Z3',2);
INSERT INTO Model VALUES('Z4',2);
INSERT INTO Model VALUES('Z8',2);

INSERT INTO Model VALUES('Ka',3);
INSERT INTO Model VALUES('Fiesta',3);
INSERT INTO Model VALUES('Focus',3);
INSERT INTO Model VALUES('Focus ST',3);
INSERT INTO Model VALUES('Focus RS',3);
INSERT INTO Model VALUES('GT',3);
INSERT INTO Model VALUES('B-Max',3);
INSERT INTO Model VALUES('C-Max',3);
INSERT INTO Model VALUES('Mondeo',3);
INSERT INTO Model VALUES('Edge',3);
INSERT INTO Model VALUES('Escort',3);
INSERT INTO Model VALUES('Fusion',3);
INSERT INTO Model VALUES('Galaxy',3);
INSERT INTO Model VALUES('Kuga',3);
INSERT INTO Model VALUES('Mustang',3);
INSERT INTO Model VALUES('Mustang GT',3);
INSERT INTO Model VALUES('Puma',3);
INSERT INTO Model VALUES('Ranger',3);
INSERT INTO Model VALUES('Raptor',3);
INSERT INTO Model VALUES('S-Max',3);
INSERT INTO Model VALUES('Taurus',3);

INSERT INTO Model VALUES('Astra',4);
INSERT INTO Model VALUES('Corsa',4);
INSERT INTO Model VALUES('Crossland X',4);
INSERT INTO Model VALUES('Grandland X',4);
INSERT INTO Model VALUES('Insignia',4);
INSERT INTO Model VALUES('Kadett',4);
INSERT INTO Model VALUES('Manta',4);
INSERT INTO Model VALUES('Mokka',4);
INSERT INTO Model VALUES('Omega',4);
INSERT INTO Model VALUES('Tigra',4);
INSERT INTO Model VALUES('Vectra',4);
INSERT INTO Model VALUES('Zafira',4);

INSERT INTO Model VALUES('204',5);
INSERT INTO Model VALUES('205',5);
INSERT INTO Model VALUES('206',5);
INSERT INTO Model VALUES('207',5);
INSERT INTO Model VALUES('208',5);
INSERT INTO Model VALUES('301',5);
INSERT INTO Model VALUES('302',5);
INSERT INTO Model VALUES('303',5);
INSERT INTO Model VALUES('304',5);
INSERT INTO Model VALUES('305',5);
INSERT INTO Model VALUES('306',5);
INSERT INTO Model VALUES('307',5);
INSERT INTO Model VALUES('308',5);
INSERT INTO Model VALUES('309',5);
INSERT INTO Model VALUES('3008',5);
INSERT INTO Model VALUES('401',5);
INSERT INTO Model VALUES('402',5);
INSERT INTO Model VALUES('403',5);
INSERT INTO Model VALUES('404',5);
INSERT INTO Model VALUES('405',5);
INSERT INTO Model VALUES('406',5);
INSERT INTO Model VALUES('407',5);
INSERT INTO Model VALUES('4007',5);
INSERT INTO Model VALUES('4008',5);
INSERT INTO Model VALUES('504',5);
INSERT INTO Model VALUES('505',5);
INSERT INTO Model VALUES('508',5);
INSERT INTO Model VALUES('5008',5);

INSERT INTO Model VALUES('Berlingo',6);
INSERT INTO Model VALUES('C1',6);
INSERT INTO Model VALUES('C2',6);
INSERT INTO Model VALUES('C3',6);
INSERT INTO Model VALUES('C3 Picasso',6);
INSERT INTO Model VALUES('C4',6);
INSERT INTO Model VALUES('C4 Picasso',6);
INSERT INTO Model VALUES('C4 Cactus',6);
INSERT INTO Model VALUES('C5',6);
INSERT INTO Model VALUES('C6',6);
INSERT INTO Model VALUES('C8',6);
INSERT INTO Model VALUES('DS',6);
INSERT INTO Model VALUES('DS3',6);
INSERT INTO Model VALUES('DS4',6);
INSERT INTO Model VALUES('DS5',6);
INSERT INTO Model VALUES('Saxo',6);
INSERT INTO Model VALUES('Space Tourer',6);
INSERT INTO Model VALUES('Xsara',6);
INSERT INTO Model VALUES('Xsara Picasso',6);

INSERT INTO Model VALUES('A-Razred',7);
INSERT INTO Model VALUES('B-Razred',7);
INSERT INTO Model VALUES('C-Razred',7);
INSERT INTO Model VALUES('CL-Razred',7);
INSERT INTO Model VALUES('CLA-Razred',7);
INSERT INTO Model VALUES('CLC-Razred',7);
INSERT INTO Model VALUES('CLK-Razred',7);
INSERT INTO Model VALUES('CLS-Razred',7);
INSERT INTO Model VALUES('E-Razred',7);
INSERT INTO Model VALUES('G-Razred',7);
INSERT INTO Model VALUES('GL-Razred',7);
INSERT INTO Model VALUES('GLA-Razred',7);
INSERT INTO Model VALUES('GLC-Razred',7);
INSERT INTO Model VALUES('GLC coupe',7);
INSERT INTO Model VALUES('GLE-Razred',7);
INSERT INTO Model VALUES('GLE coupe',7);
INSERT INTO Model VALUES('GLK-Razred',7);
INSERT INTO Model VALUES('GLS-Razred',7);
INSERT INTO Model VALUES('ML-Razred',7);
INSERT INTO Model VALUES('R-Razred',7);
INSERT INTO Model VALUES('S-Razred',7);
INSERT INTO Model VALUES('SL-Razred',7);
INSERT INTO Model VALUES('SLC-Razred',7);
INSERT INTO Model VALUES('SLS-Razred',7);
INSERT INTO Model VALUES('SLK-Razred',7);
INSERT INTO Model VALUES('SLR-Razred',7);
INSERT INTO Model VALUES('V-Razred',7);
INSERT INTO Model VALUES('X-Razred',7);

INSERT INTO Model VALUES('Arteon',8);
INSERT INTO Model VALUES('Beetle',8);
INSERT INTO Model VALUES('Borra',8);
INSERT INTO Model VALUES('Caddy',8);
INSERT INTO Model VALUES('CC',8);
INSERT INTO Model VALUES('EOS',8);
INSERT INTO Model VALUES('Fox',8);
INSERT INTO Model VALUES('Golf',8);
INSERT INTO Model VALUES('Golf Plus',8);
INSERT INTO Model VALUES('Jetta',8);
INSERT INTO Model VALUES('Lupo',8);
INSERT INTO Model VALUES('Passat',8);
INSERT INTO Model VALUES('Passat CC',8);
INSERT INTO Model VALUES('Phaeton',8);
INSERT INTO Model VALUES('Polo',8);
INSERT INTO Model VALUES('CrossPolo',8);
INSERT INTO Model VALUES('Scirocco',8);
INSERT INTO Model VALUES('Sharan',8);
INSERT INTO Model VALUES('Tiguan',8);
INSERT INTO Model VALUES('Touareg',8);
INSERT INTO Model VALUES('Touran',8);
INSERT INTO Model VALUES('T-Roc',8);
INSERT INTO Model VALUES('up',8);

INSERT INTO Model VALUES('Alto',9);
INSERT INTO Model VALUES('Ignis',9);
INSERT INTO Model VALUES('Murati',9);
INSERT INTO Model VALUES('Samurai',9);
INSERT INTO Model VALUES('SX4',9);
INSERT INTO Model VALUES('Vitara',9);
INSERT INTO Model VALUES('Swift',9);

INSERT INTO Model VALUES('Ascent',10);
INSERT INTO Model VALUES('Forester',10);
INSERT INTO Model VALUES('Impreza',10);
INSERT INTO Model VALUES('WRX STI',10);
INSERT INTO Model VALUES('Vivio',10);

INSERT INTO Model VALUES('Colt',11);
INSERT INTO Model VALUES('Eclipse',11);
INSERT INTO Model VALUES('Lancer',11);
INSERT INTO Model VALUES('Outlander',11);
INSERT INTO Model VALUES('Pajero',11);
INSERT INTO Model VALUES('Sigma',11);

INSERT INTO Model VALUES('Accent',12);
INSERT INTO Model VALUES('Atos',12);
INSERT INTO Model VALUES('Coupe',12);
INSERT INTO Model VALUES('Elantra',12);
INSERT INTO Model VALUES('Excel',12);
INSERT INTO Model VALUES('Genesis',12);
INSERT INTO Model VALUES('Gets',12);
INSERT INTO Model VALUES('i10',12);
INSERT INTO Model VALUES('i30',12);
INSERT INTO Model VALUES('i40',12);
INSERT INTO Model VALUES('Kona',12);
INSERT INTO Model VALUES('Lantra',12);
INSERT INTO Model VALUES('S-Coupe',12);
INSERT INTO Model VALUES('Santa Fe',12);
INSERT INTO Model VALUES('Grand Santa Fe',12);
INSERT INTO Model VALUES('Trajet',12);
INSERT INTO Model VALUES('Tucson',12);

INSERT INTO Model VALUES('Enzo',13);
INSERT INTO Model VALUES('California',13);
INSERT INTO Model VALUES('F12',13);
INSERT INTO Model VALUES('F40',13);
INSERT INTO Model VALUES('F50',13);
INSERT INTO Model VALUES('LaFerrari',13);

INSERT INTO Model VALUES('Aventador',14);
INSERT INTO Model VALUES('Diablo',14);
INSERT INTO Model VALUES('Gallardo',14);
INSERT INTO Model VALUES('Huracan',14);
INSERT INTO Model VALUES('Murcielago',14);
INSERT INTO Model VALUES('Urus',14);

INSERT INTO Model VALUES('911',15);
INSERT INTO Model VALUES('912',15);
INSERT INTO Model VALUES('914',15);
INSERT INTO Model VALUES('924',15);
INSERT INTO Model VALUES('Boxter',15);
INSERT INTO Model VALUES('Cayenne',15);
INSERT INTO Model VALUES('Cayman',15);
INSERT INTO Model VALUES('Carrera GT',15);
INSERT INTO Model VALUES('Macan',15);
INSERT INTO Model VALUES('Panamera',15);

INSERT INTO Model VALUES('Altea',16);
INSERT INTO Model VALUES('Arosa',16);
INSERT INTO Model VALUES('Arona',16);
INSERT INTO Model VALUES('Ateca',16);
INSERT INTO Model VALUES('Cordoba',16);
INSERT INTO Model VALUES('Ibiza',16);
INSERT INTO Model VALUES('Leon',16);
INSERT INTO Model VALUES('Toledo',16);

INSERT INTO Model VALUES('Almera',17);
INSERT INTO Model VALUES('Altima',17);
INSERT INTO Model VALUES('Cherry',17);
INSERT INTO Model VALUES('GT-R',17);
INSERT INTO Model VALUES('Juke',17);
INSERT INTO Model VALUES('Maxima',17);
INSERT INTO Model VALUES('Micra',17);
INSERT INTO Model VALUES('Murano',17);
INSERT INTO Model VALUES('Navara',17);
INSERT INTO Model VALUES('Note',17);
INSERT INTO Model VALUES('Pathfinder',17);
INSERT INTO Model VALUES('Pick up',17);
INSERT INTO Model VALUES('Primera',17);
INSERT INTO Model VALUES('Quasqai',17);
INSERT INTO Model VALUES('X-Trail',17);

INSERT INTO Model VALUES('Citigo',18);
INSERT INTO Model VALUES('Fabia',18);
INSERT INTO Model VALUES('Karoq',18);
INSERT INTO Model VALUES('Kodiaq',18);
INSERT INTO Model VALUES('Octavia',18);
INSERT INTO Model VALUES('Rapid',18);
INSERT INTO Model VALUES('Superb',18);
INSERT INTO Model VALUES('Yeti',18);

INSERT INTO Model VALUES('R 4',19);
INSERT INTO Model VALUES('R 5',19);
INSERT INTO Model VALUES('R 8',19);
INSERT INTO Model VALUES('R 9',19);
INSERT INTO Model VALUES('R 10',19);
INSERT INTO Model VALUES('R 11',19);
INSERT INTO Model VALUES('R 12',19);
INSERT INTO Model VALUES('R 14',19);
INSERT INTO Model VALUES('R 16',19);
INSERT INTO Model VALUES('R 18',19);
INSERT INTO Model VALUES('R 19',19);
INSERT INTO Model VALUES('R 20',19);
INSERT INTO Model VALUES('R 21',19);
INSERT INTO Model VALUES('R 25',19);
INSERT INTO Model VALUES('R 30',19);
INSERT INTO Model VALUES('Alpine',19);
INSERT INTO Model VALUES('Captur',19);
INSERT INTO Model VALUES('Clio',19);
INSERT INTO Model VALUES('Espace',19);
INSERT INTO Model VALUES('Grand Espace',19);
INSERT INTO Model VALUES('Kadjar',19);
INSERT INTO Model VALUES('Kangoo',19);
INSERT INTO Model VALUES('Koleos',19);
INSERT INTO Model VALUES('Laguna',19);
INSERT INTO Model VALUES('Megane',19);
INSERT INTO Model VALUES('Modus',19);
INSERT INTO Model VALUES('Scenic',19);
INSERT INTO Model VALUES('Grand Scenic',19);
INSERT INTO Model VALUES('Talisman',19);
INSERT INTO Model VALUES('Twingo',19);
INSERT INTO Model VALUES('Twizy',19);
INSERT INTO Model VALUES('Zoe',19);

INSERT INTO Model VALUES('Alfetta',20);
INSERT INTO Model VALUES('Brera',20);
INSERT INTO Model VALUES('Giulia',20);
INSERT INTO Model VALUES('Giulietta',20);
INSERT INTO Model VALUES('GT',20);
INSERT INTO Model VALUES('GTV',20);
INSERT INTO Model VALUES('Stelvio',20);

INSERT INTO Model VALUES('Arnage',21);
INSERT INTO Model VALUES('Azure',21);
INSERT INTO Model VALUES('Bentayga',21);
INSERT INTO Model VALUES('Continental',21);
INSERT INTO Model VALUES('Turbo',21);

INSERT INTO Model VALUES('Veyron',22);
INSERT INTO Model VALUES('Chiron',22);

INSERT INTO Model VALUES('Accord',23);
INSERT INTO Model VALUES('Civic',23);
INSERT INTO Model VALUES('CR-V',23);
INSERT INTO Model VALUES('CR-Z',23);
INSERT INTO Model VALUES('CRX',23);
INSERT INTO Model VALUES('FR-V',23);
INSERT INTO Model VALUES('HR-V',23);
INSERT INTO Model VALUES('Integra',23);
INSERT INTO Model VALUES('NSX',23);
INSERT INTO Model VALUES('Prelude',23);
INSERT INTO Model VALUES('Shuttle',23);
INSERT INTO Model VALUES('Jazz',23);

INSERT INTO Model VALUES('Brava',24);
INSERT INTO Model VALUES('Bravo',24);
INSERT INTO Model VALUES('Croma',24);
INSERT INTO Model VALUES('Multipla',24);
INSERT INTO Model VALUES('Marea',24);
INSERT INTO Model VALUES('Panda',24);
INSERT INTO Model VALUES('Punto',24);
INSERT INTO Model VALUES('Sedici',24);
INSERT INTO Model VALUES('Stilo',24);
INSERT INTO Model VALUES('Tipo',24);
INSERT INTO Model VALUES('Uno',24);
INSERT INTO Model VALUES('500',24);

INSERT INTO Model VALUES('Daimler',25);
INSERT INTO Model VALUES('E-Type',25);
INSERT INTO Model VALUES('E-Pace',25);
INSERT INTO Model VALUES('F-Pace',25);
INSERT INTO Model VALUES('F-Type',25);
INSERT INTO Model VALUES('S-Type',25);
INSERT INTO Model VALUES('X-Type',25);
INSERT INTO Model VALUES('XE',25);
INSERT INTO Model VALUES('XF',25);
INSERT INTO Model VALUES('XJ',25);
INSERT INTO Model VALUES('XJS',25);
INSERT INTO Model VALUES('XK',25);

INSERT INTO Model VALUES('CJ',26);
INSERT INTO Model VALUES('Cherokee',26);
INSERT INTO Model VALUES('Grand Cherokee',26);
INSERT INTO Model VALUES('Patriot',26);
INSERT INTO Model VALUES('Renegade',26);
INSERT INTO Model VALUES('Wrangler',26);

INSERT INTO Model VALUES('CeeD',27);
INSERT INTO Model VALUES('Optima',27);
INSERT INTO Model VALUES('Pride',27);
INSERT INTO Model VALUES('Pro_CeeD',27);
INSERT INTO Model VALUES('Rio',27);
INSERT INTO Model VALUES('Sorento',27);
INSERT INTO Model VALUES('Soul',27);
INSERT INTO Model VALUES('Spectra',27);
INSERT INTO Model VALUES('Sportage',27);
INSERT INTO Model VALUES('Stonic',27);
INSERT INTO Model VALUES('Venga',27);

INSERT INTO Model VALUES('Defender',28);
INSERT INTO Model VALUES('Discovery',28);
INSERT INTO Model VALUES('Freelander',28);
INSERT INTO Model VALUES('Range Rover',28);

INSERT INTO Model VALUES('Beta',29);
INSERT INTO Model VALUES('Delta',29);
INSERT INTO Model VALUES('Fulvia',29);
INSERT INTO Model VALUES('Kappa',29);
INSERT INTO Model VALUES('Musa',29);
INSERT INTO Model VALUES('Prisma',29);
INSERT INTO Model VALUES('Stratos',29);
INSERT INTO Model VALUES('Thesis',29);
INSERT INTO Model VALUES('Voyager',29);
INSERT INTO Model VALUES('Ypsilon',29);
INSERT INTO Model VALUES('Zeta',29);

INSERT INTO Model VALUES('CX-3',43);
INSERT INTO Model VALUES('CX-5',43);
INSERT INTO Model VALUES('CX-7',43);
INSERT INTO Model VALUES('CX-9',43);
INSERT INTO Model VALUES('Mazda2',43);
INSERT INTO Model VALUES('Mazda3',43);
INSERT INTO Model VALUES('Mazda5',43);
INSERT INTO Model VALUES('Mazda6',43);
INSERT INTO Model VALUES('MX-3',43);
INSERT INTO Model VALUES('MX-5',43);
INSERT INTO Model VALUES('MX-6',43);
INSERT INTO Model VALUES('RX-7',43);
INSERT INTO Model VALUES('RX-8',43);
INSERT INTO Model VALUES('Tribute',43);
INSERT INTO Model VALUES('Xedos 6',43);
INSERT INTO Model VALUES('Xedos 9',43);

INSERT INTO Model VALUES('9-3',44);
INSERT INTO Model VALUES('9-4x',44);
INSERT INTO Model VALUES('9-5',44);
INSERT INTO Model VALUES('9-7x',44);
INSERT INTO Model VALUES('90',44);
INSERT INTO Model VALUES('96',44);
INSERT INTO Model VALUES('99',44);
INSERT INTO Model VALUES('900',44);
INSERT INTO Model VALUES('9000',44);

INSERT INTO Model VALUES('Auris',45);
INSERT INTO Model VALUES('Avensis',45);
INSERT INTO Model VALUES('Aygo',45);
INSERT INTO Model VALUES('C-HR',45);
INSERT INTO Model VALUES('Celica',45);
INSERT INTO Model VALUES('Corolla',45);
INSERT INTO Model VALUES('Hi-Lux',45);
INSERT INTO Model VALUES('Land Cruiser',45);
INSERT INTO Model VALUES('Prius',45);
INSERT INTO Model VALUES('RAV4',45);
INSERT INTO Model VALUES('Supra',45);
INSERT INTO Model VALUES('Verso',45);
INSERT INTO Model VALUES('Yaris',45);


INSERT INTO Model VALUES('C30',46);
INSERT INTO Model VALUES('C70',46);
INSERT INTO Model VALUES('S40',46);
INSERT INTO Model VALUES('S60',46);
INSERT INTO Model VALUES('S70',46);
INSERT INTO Model VALUES('S80',46);
INSERT INTO Model VALUES('S90',46);
INSERT INTO Model VALUES('V40',46);
INSERT INTO Model VALUES('V50',46);
INSERT INTO Model VALUES('V60',46);
INSERT INTO Model VALUES('V70',46);
INSERT INTO Model VALUES('V90',46);
INSERT INTO Model VALUES('XC40',46);
INSERT INTO Model VALUES('XC60',46);
INSERT INTO Model VALUES('XC70',46);
INSERT INTO Model VALUES('XC90',46);

INSERT INTO Model VALUES('Roadster',47);
INSERT INTO Model VALUES('Model 3',47);
INSERT INTO Model VALUES('Model S',47);
INSERT INTO Model VALUES('Model X',47);

INSERT INTO Model VALUES('Biturbo',48);
INSERT INTO Model VALUES('Coupe',48);
INSERT INTO Model VALUES('GranSport',48);
INSERT INTO Model VALUES('GranTurismo',48);
INSERT INTO Model VALUES('GranCabrio',48);
INSERT INTO Model VALUES('Levante',48);
INSERT INTO Model VALUES('Quattroporte',48);
INSERT INTO Model VALUES('Spyder',48);

INSERT INTO Model VALUES('Neki',80);











select * from Model;
select * from Znamka z, Model m where z.ID_znamke = m.ID_znamke and z.ID_znamke = 48;


INSERT INTO Znamka VALUES('Audi');
INSERT INTO Znamka VALUES('BMW');
INSERT INTO Znamka VALUES('Ford');
INSERT INTO Znamka VALUES('Opel');
INSERT INTO Znamka VALUES('Peugeot');
INSERT INTO Znamka VALUES('Citroen');
INSERT INTO Znamka VALUES('Mercedes');
INSERT INTO Znamka VALUES('Volkswagen');
INSERT INTO Znamka VALUES('Suzuki');
INSERT INTO Znamka VALUES('Subaru');
INSERT INTO Znamka VALUES('Michubishi');
INSERT INTO Znamka VALUES('Hyundai');
INSERT INTO Znamka VALUES('Ferrari');
INSERT INTO Znamka VALUES('Lamborghini');
INSERT INTO Znamka VALUES('Porsche');
INSERT INTO Znamka VALUES('Seat');
INSERT INTO Znamka VALUES('Nissan');
INSERT INTO Znamka VALUES('Skoda');
INSERT INTO Znamka VALUES('Renault');
INSERT INTO Znamka VALUES('Alfa Romeo');
INSERT INTO Znamka VALUES('Bentley');
INSERT INTO Znamka VALUES('Bugatti');
INSERT INTO Znamka VALUES('Honda');
INSERT INTO Znamka VALUES('Fiat');
INSERT INTO Znamka VALUES('Jaguar');
INSERT INTO Znamka VALUES('Jeep');
INSERT INTO Znamka VALUES('Kia');
INSERT INTO Znamka VALUES('Land rover');
INSERT INTO Znamka VALUES('Lancia');
INSERT INTO Znamka VALUES('Mazda'); 43
INSERT INTO Znamka VALUES('Saab');  44
INSERT INTO Znamka VALUES('Toyota');45
INSERT INTO Znamka VALUES('Volvo'); 46
INSERT INTO Znamka VALUES('Tesla'); 47
INSERT INTO Znamka VALUES('Maserati');48

ALTER TABLE Uporabnik
ADD CONSTRAINT AK_Uporabnikuniv UNIQUE(Uporabnisko_ime);


<connectionStrings>
    <add name="DefaultConnection" connectionString="Data Source=(LocalDb)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\aspnet-WebApplication2-20180118061053.mdf;Initial Catalog=aspnet-WebApplication2-20180118061053;Integrated Security=True" providerName="System.Data.SqlClient" />
  </connectionStrings>



CREATE TABLE Poslovalnica(
ID_poslovalnice integer not null IDENTITY(1,1),
Naziv varchar(50) not null,
Naslov varchar(50) not null,
ID_kraja integer not null,
primary key(ID_poslovalnice)
foreign key(ID_kraja) REFERENCES Kraj(ID_kraja));


INSERT INTO Poslovalnica VALUES('Avtoservis Knap','Dunajska cesta 20 Ljubljana');

create table Kraj(
ID_kraja integer not null IDENTITY(1,1),
Naziv_kraja varchar(50),
primary key(ID_kraja));


INSERT INTO Kraj VALUES('Ljubljana');
INSERT INTO Kraj VALUES('Maribor');
INSERT INTO Kraj VALUES('Kranj');
INSERT INTO Kraj VALUES('Celje');
INSERT INTO Kraj VALUES('Velenje');
INSERT INTO Kraj VALUES('Murska sobota');
INSERT INTO Kraj VALUES('Kocevje');
INSERT INTO Kraj VALUES('Jesenice');
INSERT INTO Kraj VALUES('Kranjska gora');
INSERT INTO Kraj VALUES('Ptuj');

INSERT INTO Poslovalnica VALUES('Avtoservis Knap','Dunajska cesta 20',1);
INSERT INTO Poslovalnica VALUES('Avtoservis Peter','Smartinska cesta 50',1);
INSERT INTO Poslovalnica VALUES('Avtoservis Guma','Presernova cesta 15',1);
INSERT INTO Poslovalnica VALUES('Avtoservis Gams','Litostrojska cesta 5',1);
INSERT INTO Poslovalnica VALUES('Avtoservis Gorazd','Nomadska cesta 16',1);

INSERT INTO Poslovalnica VALUES('Avtoservis Maribor','Mariborska cesta 1',2);
INSERT INTO Poslovalnica VALUES('Avtoservis Pohorje','Majska cesta 40',2);
INSERT INTO Poslovalnica VALUES('Avtoservis Lisica','Industrijska cesta 12',2);

INSERT INTO Poslovalnica VALUES('Avtoservis Kocna','Mala cesta 14',3);
INSERT INTO Poslovalnica VALUES('Avtoservis Gorenc','Skrlatna cesta 15',3);

INSERT INTO Poslovalnica VALUES('Avtoservis Celje','Celjska cesta 8',4);
INSERT INTO Poslovalnica VALUES('Avtoservis Macesen','Gornja cesta 5',4);

INSERT INTO Poslovalnica VALUES('Avtoservis Velenje','Velika cesta 2',5);
INSERT INTO Poslovalnica VALUES('Avtoservis Klinar','Loska cesta 8',5);

INSERT INTO Poslovalnica VALUES('Avtoservis Prekmurje','Sobotna cesta 2',6);
INSERT INTO Poslovalnica VALUES('Avtoservis Gaj','Prekmurska cesta 18',6);

INSERT INTO Poslovalnica VALUES('Avtoservis Kocevje','Jezerna cesta 15',7);

INSERT INTO Poslovalnica VALUES('Avtoservis Jesenice','Jeseniska cesta 9',8);

INSERT INTO Poslovalnica VALUES('Avtoservis Razor','Gorska cesta 7',9);

INSERT INTO Poslovalnica VALUES('Avtoservis Ptuj','Ptujska cesta 10',10);
INSERT INTO Poslovalnica VALUES('Avtoservis Grad','Ledvena cesta 7',10);

//Ljubljana
INSERT INTO Uporabnik VALUES('Knapko','knapko');
INSERT INTO Uporabnik VALUES('lovro','lovro');
INSERT INTO Uporabnik VALUES('konrad','konrad');


INSERT INTO Uporabnik VALUES('Peter','zvezda');
INSERT INTO Uporabnik VALUES('Zdravko','kljun');

INSERT INTO Uporabnik VALUES('jolom','parina');

INSERT INTO Uporabnik VALUES('gorko','smeh');
INSERT INTO Uporabnik VALUES('koki','kebla');

INSERT INTO Uporabnik VALUES('ziga','zogica');
INSERT INTO Uporabnik VALUES('bor','nomad');

//Maribor
INSERT INTO Uporabnik VALUES('koren','petnajst');
INSERT INTO Uporabnik VALUES('goran','zvestoba');

INSERT INTO Uporabnik VALUES('urh','mozaik');

INSERT INTO Uporabnik VALUES('klika','petnajst');

//Kranj

INSERT INTO Uporabnik VALUES('gorenc','petarda');

INSERT INTO Uporabnik VALUES('gorenc2','listje');

//Celje
INSERT INTO Uporabnik VALUES('celjan1','celje');
INSERT INTO Uporabnik VALUES('celjan2','obcina');

//Velenje
INSERT INTO Uporabnik VALUES('velencan','velopolje');
INSERT INTO Uporabnik VALUES('velencan2','krajina');

//Murska sobota
INSERT INTO Uporabnik VALUES('prekmurc1','prekmurje');
INSERT INTO Uporabnik VALUES('prekmurc2','mura');

//Kocevje
INSERT INTO Uporabnik VALUES('serviser1','reka');

//Jesenice
INSERT INTO Uporabnik VALUES('jesenican1','jesen');

//Kranjska gora
INSERT INTO Uporabnik VALUES('gorjan1','prisank');

//Ptuj
INSERT INTO Uporabnik VALUES('ptujcan1','pivka');
INSERT INTO Uporabnik VALUES('ptujcan2','jama');

// Zaposleni
//Ljubljana
INSERT INTO Zaposleni VALUES('Boris','Knap',1,3);
INSERT INTO Zaposleni VALUES('Lovro','Kuhar',1,4);
INSERT INTO Zaposleni VALUES('Konrad','Vesel',1,5);

INSERT INTO Zaposleni VALUES('Peter','Poles',2,6);
INSERT INTO Zaposleni VALUES('Zdravko','Kljun',2,7);

INSERT INTO Zaposleni VALUES('Jolom','Pevec',3,8);

INSERT INTO Zaposleni VALUES('Gorazd','Tic',4,9);
INSERT INTO Zaposleni VALUES('Kristjan','Bergant',4,10);

INSERT INTO Zaposleni VALUES('Ziga','Kobal',5,11);
INSERT INTO Zaposleni VALUES('Bor','Zonta',5,12);

//Maribor
INSERT INTO Zaposleni VALUES('Luka','Konta',6,13);
INSERT INTO Zaposleni VALUES('Jure','Mesetek',6,14);

INSERT INTO Zaposleni VALUES('Urh','Konec',7,15);

INSERT INTO Zaposleni VALUES('Klara','Bogataj',8,16);

//Kranj
INSERT INTO Zaposleni VALUES('Luka','Sluga',9,17);

INSERT INTO Zaposleni VALUES('Goran','Dragic',10,18);

//Celje
INSERT INTO Zaposleni VALUES('Miran','Stanovnik',11,19);

INSERT INTO Zaposleni VALUES('Marjan','Sarec',12,20);

//Velenje
INSERT INTO Zaposleni VALUES('Mira','Trstenak',13,21);

INSERT INTO Zaposleni VALUES('Mojca','Borec',14,22);

//Murska sobota
INSERT INTO Zaposleni VALUES('Jan','Mesen',15,23);

INSERT INTO Zaposleni VALUES('Zan','Lovsin',16,24);

//Kocevje
INSERT INTO Zaposleni VALUES('Zan','Mali',17,25);

//Jesenice
INSERT INTO Zaposleni VALUES('Grega','Skocir',18,26);

//Kranjska gora
INSERT INTO Zaposleni VALUES('Denis','Mlakar',19,27);

//Ptuj
INSERT INTO Zaposleni VALUES('Urban','Mestek',20,28);
INSERT INTO Zaposleni VALUES('Bostjan','Kline',21,29);














CREATE TABLE Zaposleni(
ID_zaposleni integer not null IDENTITY(1,1),
Ime varchar(15) not null,
Priimek varchar(20) not null,
ID_poslovalnice integer not null,
ID_uporabnika integer not null,
primary key(ID_zaposleni),
foreign key(ID_poslovalnice) REFERENCES Poslovalnica(ID_poslovalnice),
foreign key(ID_uporabnika) REFERENCES Uporabnik(ID_uporabnika));








































