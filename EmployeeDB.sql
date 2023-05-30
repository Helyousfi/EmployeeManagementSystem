USE EMSDB

IF OBJECT_ID('udp_SelectAllEmployees', 'P') IS NOT NULL
    DROP PROCEDURE udp_SelectAllEmployees;

IF OBJECT_ID('udp_SelectEmployeeById', 'P') IS NOT NULL
    DROP PROCEDURE udp_SelectEmployeeById;

IF OBJECT_ID('udp_InsertEmployee', 'P') IS NOT NULL
    DROP PROCEDURE udp_InsertEmployee;

IF OBJECT_ID('udp_UpdateEmployee', 'P') IS NOT NULL
    DROP PROCEDURE udp_UpdateEmployee;

IF OBJECT_ID('udp_DeleteEmployee', 'P') IS NOT NULL
    DROP PROCEDURE udp_DeleteEmployee;

GO

CREATE PROCEDURE udp_SelectAllEmployees
AS 
BEGIN
    SELECT * FROM Employee;
END;

GO

CREATE PROCEDURE udp_SelectEmployeeById
(
    @Id int
)
AS
BEGIN
    SELECT * FROM Employee WHERE Id = @Id;
END; 

GO

create procedure udp_InsertEmployee
(
	@Id int,
	@Name Varchar(30),
	@Age int,
	@Email varchar(50),
	@Phone varchar(10),
	@Job varchar(50)
)
as insert into Employee values (@Id, @Name, @Age, @Email, @Phone, @Job)

GO

create procedure udp_UpdateEmployee
(
	@Id int,
	@Name Varchar(30),
	@Age int,
	@Email varchar(50),
	@Phone varchar(10),
	@Job varchar(50)
)
as update Employee set Name=@Name, Age=@Age, Email=@Email, Phone=@Phone, Job=@Job where Id = @Id

GO

create procedure udp_DeleteEmployee
(
	@Id int
)
as delete from Employee where Id=@Id;
GO