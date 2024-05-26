INSERT INTO Client (FirstName, LastName, Email, Telephone, Pesel) VALUES
                                                                      ('John', 'Doe', 'john.doe@example.com', '123-456-789', '90010112345'),
                                                                      ('Jane', 'Smith', 'jane.smith@example.com', '987-654-321', '80020254321'),
                                                                      ('Adam', 'Johnson', 'adam.johnson@example.com', '555-123-456', '85030367890'),
                                                                      ('Test', 'Test', 'adsadf.johnson@example.com', '55235-123-456', '85031410367890');


INSERT INTO Country (Name) VALUES
                               ('Poland'),
                               ('Germany'),
                               ('France');


INSERT INTO Trip (Name, Description, DateFrom, DateTo, MaxPeople) VALUES
                                                                      ('Trip to Poland and Germany', 'A wonderful trip through Poland and Germany', '2024-06-01', '2024-06-15', 20),
                                                                      ('Trip to France', 'Experience the beauty of France', '2024-07-01', '2024-07-10', 15);


INSERT INTO Client_Trip (IdClient, IdTrip, RegisteredAt, PaymentDate) VALUES
                                                                          (1, 1, '2024-05-01', NULL),
                                                                          (2, 1, '2024-05-02', '2024-05-10'),
                                                                          (3, 2, '2024-05-03', '2024-05-15');


INSERT INTO Country_Trip (IdCountry, IdTrip) VALUES
                                                 (1, 1),
                                                 (2, 1),
                                                 (3, 2);