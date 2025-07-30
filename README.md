# Важная информация

Протестировать приложение можно через Swagger:

<img width="1689" height="398" alt="image" src="https://github.com/user-attachments/assets/dd67eb10-dac8-4a57-b40d-28e26cce33be" />

Первый запрос (Create), в отличии от остальных (с реквестом и репозиторием) содержит в себе доп. функционал для демонстрации имеющихся навыков, а именно:
1) Маппер
2) Класс для респонса OperationResultResponse<T>
3) Валидатор
5) Команда (обёртка для респонса, валидации и выполнения запроса)

<img width="603" height="685" alt="image" src="https://github.com/user-attachments/assets/786ce72a-6b82-4b5f-adb4-d9ffe9f9214f" />


Пример успешного запроса:
<img width="1615" height="510" alt="image" src="https://github.com/user-attachments/assets/ac7a9da4-0e54-48ae-b7d3-fc62a3f38f02" />

<img width="340" height="278" alt="image" src="https://github.com/user-attachments/assets/50875470-b353-4a64-b9f8-30c456bfff38" />

Пример неуспешной валидации:

<img width="1583" height="478" alt="image" src="https://github.com/user-attachments/assets/0f9fdf2b-5b05-4c92-97ec-b1e19a78b971" />

<img width="499" height="411" alt="image" src="https://github.com/user-attachments/assets/29c57af5-df9a-4bcd-bfec-c712847ec311" />


Строка подключения к бд находиться в классе Startup:

<img width="860" height="366" alt="image" src="https://github.com/user-attachments/assets/10e106d6-b760-4cd6-b0c8-2c87e006fb6e" />

Структура таблицы для теста:

CREATE TABLE EmployeesDb (
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

CREATE TABLE DepartmentsDb (
    Id INT PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(20)
);

INSERT INTO Departments(Id, Name, Phone)
            VALUES (1, 'RABBIT', '123');

INSERT INTO DepartmentsDb(Name, Phone)
            VALUES ('KAFKA', '123');

            
INSERT INTO EmployeesDb (Name, Surname, Phone, CompanyId, DepartmentId, PassportType, PassportNumber)
            VALUES ('Сергей', 'Сес', '+7914', 5, 4, 'Rus', 123);

select * from EmployeesDb as emp
join DepartmentsDb dep on emp.DepartmentId = dep.Id

select * from DepartmentsDb
