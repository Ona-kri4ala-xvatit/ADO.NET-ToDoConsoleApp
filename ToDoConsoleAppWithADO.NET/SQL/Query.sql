Create database ToDoListDB
Use ToDoListDB

Create table Tasks
(
	[Id] int primary key identity,
	[Name] nvarchar(max) not null,
	CHECK([Name] != '')
)

Insert into Tasks([Name])
values('Eat'), ('Drink Beer'), ('Drink Vodka'), ('Meet the Girls')

Create table Priorities
(
	[Id] int primary key identity,
	[Name] nvarchar(20) not null,
	[TaskId] int references Tasks([Id]) not null,
	CHECK([Name] != '')
)

Insert into Priorities([Name],[TaskId])
Values('Low', 1), ('High', 2), ('Unknown', 3), ('Critical', 4)

Create table TaskStatuses
(
	[Id] int primary key identity,
	[Name] nvarchar(20) not null,
	[TaskId] int references Tasks([Id]) not null,
	CHECK([Name] != '')
)

Insert into TaskStatuses([Name], [TaskId])
Values('In Progress', 1), ('In Progress', 2), ('Complete', 3), ('In Progress', 4) 
