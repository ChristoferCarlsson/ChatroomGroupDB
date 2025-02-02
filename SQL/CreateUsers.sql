---- Create Customers table
CREATE TABLE Users (
    UserId INT PRIMARY KEY,
    UserName VARCHAR(255) NOT NULL,
    UserPassword VARBINARY(MAX) NOT NULL,  -- Encrypted password
    Email VARCHAR(255) NOT NULL
);

-- Create Posts table
CREATE TABLE Posts (
    PostId INT PRIMARY KEY,
	Text VARCHAR(255) NOT NULL,
	UserId INT,
	FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

-- Insert Dummy Data
INSERT INTO Users (UserId, UserName, UserPassword, Email) VALUES
(1, 'John21', ENCRYPTBYPASSPHRASE('MySecretKey', 'Password123'), 'john.doe@example.com'),
(2, 'Dog_Lover', ENCRYPTBYPASSPHRASE('MySecretKey', 'CaninesRulz'), 'jane.smith@example.com'),
(3, 'LarsTheMan', ENCRYPTBYPASSPHRASE('MySecretKey', '11223344'),'robert.brown@example.com');

INSERT INTO Posts (PostId, Text, UserId) VALUES
(1, 'Hello world! This is my first post.', 1),
(2, 'Just had a great coffee this morning!', 2),
(3, 'Anyone watching the game tonight?', 3),
(4, 'Learning SQL is so much fun!', 2),
(5, 'Looking for book recommendations.', 3);

CREATE INDEX IX_Posts_UserId ON Posts(UserId);