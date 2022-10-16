CREATE TABLE  NoticeBoard (
    Id INT PRIMARY KEY IDENTITY(1,1),
	Title  VARCHAR (200),
    Notice VARCHAR (max) NOT NULL,
    PublishedFrom DATE NOT NULL,
	PublishedTo DATE NOT NULL,
	DisplayForm bit,
	ThankYou VARCHAR (max)  NULL,
	CreatedOnUtc DATE  NULL
);
go

CREATE TABLE NoticeBoardDetail (
    Id int NOT NULL PRIMARY KEY,
    Name VARCHAR (350) NOT NULL,
	 PhoneNumber VARCHAR (12) NOT NULL,
	  EmailAddress VARCHAR (350) NOT NULL,
	  City VARCHAR (250) NOT NULL,
    NoticeBoardId int FOREIGN KEY REFERENCES NoticeBoard(Id),
	  Notice VARCHAR (max)  NULL,
	CreatedOnUtc DATE not NULL
);

