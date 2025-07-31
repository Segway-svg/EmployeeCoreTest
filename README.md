# Важная информация

Протестировать приложение можно через Swagger:

<img width="1689" height="398" alt="image" src="https://github.com/user-attachments/assets/dd67eb10-dac8-4a57-b40d-28e26cce33be" />

Для каждого запроса реализован свой HTTP-реквест и репозиторий для работы с базами данных

А для запроса Create и Update также реализован доп. функционал, а именно:

1) Маппер (Запрос API -> Сущность БД)
2) Класс для респонса OperationResultResponse<T> (обёртка для детального ответа / отображения ошибок)
3) Валидатор (проверка значений реквеста)
5) Команда (бизнес логика, обёртка для респонса, валидации и работы с репозиторием)

<img width="603" height="685" alt="image" src="https://github.com/user-attachments/assets/786ce72a-6b82-4b5f-adb4-d9ffe9f9214f" />


Пример успешного запроса:
<img width="1615" height="510" alt="image" src="https://github.com/user-attachments/assets/ac7a9da4-0e54-48ae-b7d3-fc62a3f38f02" />

<img width="340" height="278" alt="image" src="https://github.com/user-attachments/assets/50875470-b353-4a64-b9f8-30c456bfff38" />

Пример неуспешной валидации:

<img width="1583" height="478" alt="image" src="https://github.com/user-attachments/assets/0f9fdf2b-5b05-4c92-97ec-b1e19a78b971" />

<img width="499" height="411" alt="image" src="https://github.com/user-attachments/assets/29c57af5-df9a-4bcd-bfec-c712847ec311" />


Подключений к бд реализовано в классе DbConnection (строка подключения в appsettings.json):

<img width="832" height="375" alt="image" src="https://github.com/user-attachments/assets/33e19607-31be-47bf-86ec-08c670667450" />

DI и прочие настройки проекта располагаются в классе *Startup*

Структура таблицы для теста:

┌───────────────────────┐       ┌───────────────────────┐
│     DepartmentsDb     │       │       EmployeesDb     │
├───────────────────────┤       ├───────────────────────┤
│ PK │ Id           INT ├───────┼ FK │ DepartmentId INT │
│    │ Name     NVARCHAR│       │ PK │ Id        INT    │
│    │ Phone    NVARCHAR│       │    │ Name    NVARCHAR │
└───────────────────────┘       │    │ Surname NVARCHAR │
                                │    │ Phone   NVARCHAR │
                                │    │ CompanyId INT    │
                                │    │ PassportType     │
                                │    │ PassportNumber   │
                                └───────────────────────┘

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

INSERT INTO DepartmentsDb(Id, Name, Phone)
            VALUES (2, 'KAFKA', '123');

            
INSERT INTO EmployeesDb (Name, Surname, Phone, CompanyId, DepartmentId, PassportType, PassportNumber)
            VALUES ('Сергей', 'Сес', '+7914', 5, 2, 'Rus', 123);

select * from EmployeesDb as emp
join DepartmentsDb dep on emp.DepartmentId = dep.Id

select * from DepartmentsDb
