-- Create Users table
CREATE TABLE IF NOT EXISTS Users (
    Id UUID PRIMARY KEY,
    Username VARCHAR(64) NOT NULL,
    Password VARCHAR(64) NOT NULL
);

-- Create Meetings table
CREATE TABLE IF NOT EXISTS Meetings (
    Id UUID PRIMARY KEY,
    Title VARCHAR(128) NOT NULL,
    StartDate TIMESTAMP NOT NULL,
    EndDate TIMESTAMP NOT NULL
);

-- Insert seed data for Users
INSERT INTO Users (Id, Username, Password) VALUES
    (gen_random_uuid (), 'admin', 'password');

-- Insert seed data for Meetings
INSERT INTO Meetings (Id, Title, StartDate, EndDate) VALUES
    (gen_random_uuid (), 'Test meeting 1', '2023-11-19T13:30:00', '2023-11-19T15:00:00'),
    (gen_random_uuid (), 'Test meeting 2', '2023-11-19T19:00:00', '2023-11-19T19:15:00'),
    (gen_random_uuid (), 'Test meeting 3', '2023-11-20T16:00:00', '2023-11-20T17:30:00');