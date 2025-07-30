# Важная информация

Протестировать приложение можно через Swagger:

<img width="1689" height="398" alt="image" src="https://github.com/user-attachments/assets/dd67eb10-dac8-4a57-b40d-28e26cce33be" />


Строка подключения к бд находиться в классе Startup:

<img width="860" height="366" alt="image" src="https://github.com/user-attachments/assets/10e106d6-b760-4cd6-b0c8-2c87e006fb6e" />

Структура таблицы для теста:

CREATE TABLE Employees (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Surname NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(20),
    CompanyId INT NOT NULL,
    DepartmentName NVARCHAR(100),
    DepartmentPhone NVARCHAR(20),
    PassportType NVARCHAR(50),
    PassportNumber NVARCHAR(50)
);

На данный момент переделываю к более нормализованному виду:

CREATE TABLE Employees (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Surname NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(20),
    CompanyId INT NOT NULL,
    DepartmentId INT NOT NULL,
    PassportType NVARCHAR(50),
    PassportNumber NVARCHAR(50),
    CONSTRAINT FK_Employee_Department_Db FOREIGN KEY (DepartmentId) REFERENCES Departments(Id)
);

CREATE TABLE Departments (
    Id INT PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(20)
);
