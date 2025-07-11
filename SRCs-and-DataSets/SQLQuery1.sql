create table tbLogin(
username varchar(50) primary key,
password varchar(50) not null,
role varchar(50)
)

create table tblDepartment(
DepID int identity(1,1) primary key,
DeptName varchar(50),
DeptDesc varchar(200),
DeptDuration int
)

create table tblFees(
FID int identity(1,1) primary key,
StdID int,
Stdname varchar(50),
Fdate date,
Amount int
)

create table tblStudent(
StdID int identity(1,1) primary key,
Stdname varchar(50),
StdGender varchar(50),
StdDOB date,
StdPhone varchar(50),
StdDept varchar(50),
StdFees int
)

create table tblTeacher(
TID int identity(1,1) primary key,
Tname varchar(50),
TGender varchar(50),
TDOB date,
TPhone varchar(50),
TDept varchar(50),
TAddress varchar(200)
)


insert into  tblDepartment(DeptName, DeptDesc, DeptDuration)
values('CS','Computer Science',3),
('Civil', 'Civil',4)

select * from tblDepartment




-- Add FK from tblStudent to tblDepartment
ALTER TABLE tblStudent
ADD CONSTRAINT FK_Student_Department
FOREIGN KEY (StdDept) REFERENCES tblDepartment(DepID);

-- Add FK from tblTeacher to tblDepartment
ALTER TABLE tblTeacher
ADD CONSTRAINT FK_Teacher_Department
FOREIGN KEY (TDept) REFERENCES tblDepartment(DepID);

-- Add FK from tblFees to tblStudent
ALTER TABLE tblFees
ADD CONSTRAINT FK_Fees_Student
FOREIGN KEY (StdID) REFERENCES tblStudent(StdID);




ALTER TABLE tblStudent
ALTER COLUMN StdDept INT;

ALTER TABLE tblTeacher
ALTER COLUMN TDept INT;



SELECT name
FROM sys.foreign_keys
WHERE parent_object_id = OBJECT_ID('tblFees');

ALTER TABLE tblFees
DROP CONSTRAINT FK_Fees_Student;

ALTER TABLE tblFees
ADD CONSTRAINT FK_Fees_Student
FOREIGN KEY (StdID) REFERENCES tblStudent(StdID);

SELECT * FROM tblDepartment;
