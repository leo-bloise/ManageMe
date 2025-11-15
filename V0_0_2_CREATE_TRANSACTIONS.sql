CREATE TABLE Transactions (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Amount DECIMAL(19, 4) NOT NULL,
	Description TEXT NOT NULL,
	Movement INT CHECK (Movement in (0, 1)) NOT NULL,
	UserId INT NOT NULL REFERENCES Users(Id)
);