PRAGMA foreign_keys = ON;

-- Vymazani predchozich testovacich dat z tohoto skriptu
DELETE FROM "StarExoplanets"
WHERE "StarId" BETWEEN 1001 AND 1999
   OR "ExoplanetId" BETWEEN 1001 AND 1999;

DELETE FROM "Exoplanets" WHERE "Id" BETWEEN 1001 AND 1999;
DELETE FROM "Stars" WHERE "Id" BETWEEN 1001 AND 1999;
DELETE FROM "Nebulae" WHERE "Id" BETWEEN 1001 AND 1999;
DELETE FROM "StarSystems" WHERE "Id" BETWEEN 1001 AND 1999;

-- Enum hodnoty:
-- SpectralClass: O=1, B=2, A=3, F=4, G=5, K=6, M=7
-- ExoplanetType: Terrestrial=1, SuperEarth=2, NeptuneLike=3, GasGiant=4
-- NebulaType: Emission=1, Reflection=2, Dark=3, Planetary=4, SupernovaRemnant=5

INSERT INTO "StarSystems"
("Id", "Name", "DistanceLy", "Coordinates_Rectascension", "Coordinates_Declination")
VALUES
(1001, 'Proxima Centauri', 4.2465, '14h 29m 43s', '-62d 40m 46s'),
(1002, 'TRAPPIST-1', 40.66, '23h 06m 29s', '-05d 02m 29s'),
(1003, '51 Pegasi', 50.45, '22h 57m 28s', '+20d 46m 08s'),
(1004, 'Kepler-90', 2840.0, '18h 57m 44s', '+49d 18m 19s'),
(1005, 'Theta1 Orionis', 1350.0, '05h 35m 16s', '-05d 23m 23s');

INSERT INTO "Nebulae"
("Id", "Name", "Type", "DistanceLy", "Coordinates_Rectascension", "Coordinates_Declination")
VALUES
(1001, 'Orion Nebula', 1, 1344.0, '05h 35m 17s', '-05d 23m 28s'),
(1002, 'Ring Nebula', 4, 2567.0, '18h 53m 35s', '+33d 01m 45s'),
(1003, 'Crab Nebula', 5, 6500.0, '05h 34m 31s', '+22d 00m 52s');

INSERT INTO "Stars"
("Id", "Name", "SpectralClass", "Mass", "Age", "StarSystemId", "NebulaId")
VALUES
(1001, 'Proxima Centauri', 7, 0.122, 4.85, 1001, NULL),
(1002, 'TRAPPIST-1', 7, 0.089, 7.60, 1002, NULL),
(1003, '51 Pegasi', 5, 1.11, 6.10, 1003, NULL),
(1004, 'Kepler-90', 5, 1.13, 2.00, 1004, NULL),
(1005, 'Theta1 Orionis C', 1, 33.0, 0.20, 1005, 1001);

INSERT INTO "Exoplanets"
("Id", "Name", "Mass", "OrbitTime", "Type", "StarSystemId")
VALUES
(1001, 'Proxima Centauri b', 1.27, 11.186, 1, 1001),
(1002, 'TRAPPIST-1 d', 0.388, 4.049, 1, 1002),
(1003, 'TRAPPIST-1 e', 0.692, 6.101, 1, 1002),
(1004, '51 Pegasi b', 150.0, 4.231, 4, 1003),
(1005, 'Kepler-90 i', 2.5, 14.449, 2, 1004);

INSERT INTO "StarExoplanets"
("StarId", "ExoplanetId")
VALUES
(1001, 1001),
(1002, 1002),
(1002, 1003),
(1003, 1004),
(1004, 1005);

