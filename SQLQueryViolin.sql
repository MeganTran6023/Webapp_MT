-- make table 

-- create table
CREATE TABLE students (
    id INT NOT NULL PRIMARY KEY IDENTITY,
    name VARCHAR (100) NOT NULL,
    email VARCHAR (150) NOT NULL UNIQUE,
    age VARCHAR(20) NULL,
    yearsPlayed VARCHAR(20) NULL,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);
-- components (insert)
INSERT INTO students (name, email, age, yearsPlayed)
VALUES
    ('John Doe', 'john.doe@example.com', '25', '5'),
    ('Jane Smith', 'jane.smith@example.com', '30', '10'),
    ('David Lee', 'david.lee@example.com', '22', '3'),
    ('Emily Brown', 'emily.brown@example.com', '28', '8'),
    ('Michael Davis', 'michael.davis@example.com', '19', '2');