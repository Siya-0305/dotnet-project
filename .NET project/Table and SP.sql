CREATE TABLE MOM_Department (
    DepartmentID INT IDENTITY(1,1) PRIMARY KEY,
    DepartmentName NVARCHAR(100) NOT NULL UNIQUE,
    Created DATETIME DEFAULT GETDATE(),
    Modified DATETIME NOT NULL
);
--insert--
CREATE PROCEDURE SP_Insert_MOM_Department
    @DepartmentName NVARCHAR(100)
AS
BEGIN
    INSERT INTO MOM_Department (DepartmentName, Created, Modified)
    VALUES (@DepartmentName, GETDATE(), GETDATE())
END
EXEC SP_Insert_MOM_Department 
    @DepartmentName = 'IT'
EXEC SP_Insert_MOM_Department 
    @DepartmentName = 'CE'
EXEC SP_Insert_MOM_Department 
    @DepartmentName = 'BCA'
EXEC SP_Insert_MOM_Department 
    @DepartmentName = 'BBA'
EXEC SP_Insert_MOM_Department 
    @DepartmentName = 'DE'
EXEC SP_Insert_MOM_Department 
    @DepartmentName = 'MCA'
EXEC SP_Insert_MOM_Department 
    @DepartmentName = 'MBA'

select * from MOM_Department

--update--
CREATE PROCEDURE SP_Update_MOM_Department
    @DepartmentID INT,
    @DepartmentName NVARCHAR(100)
AS
BEGIN
    UPDATE MOM_Department
    SET DepartmentName = @DepartmentName,
        Modified = GETDATE()
    WHERE DepartmentID = @DepartmentID
END
EXEC SP_Update_MOM_Department 
    @DepartmentID = 1,
    @DepartmentName = 'Human Resources'

--delete--
CREATE PROCEDURE SP_Delete_MOM_Department
    @DepartmentID INT
AS
BEGIN
    DELETE FROM MOM_Department
    WHERE DepartmentID = @DepartmentID
END
EXEC SP_Delete_MOM_Department 
    @DepartmentID = 3

--SelectAll--
CREATE or alter PROCEDURE SP_SelectAll_MOM_Department
AS
BEGIN
    SELECT DepartmentID,DepartmentName,Created,Modified FROM MOM_Department
END

--SelectByID--
CREATE PROCEDURE SP_SelectByID_MOM_Department
    @DepartmentID INT
AS
BEGIN
    SELECT * FROM MOM_Department
    WHERE DepartmentID = @DepartmentID
END
EXEC SP_SelectByID_MOM_Department 
    @DepartmentID = 1

--MOM_MeetingType Table--
CREATE TABLE MOM_MeetingType (
    MeetingTypeID INT IDENTITY(1,1) PRIMARY KEY,
    MeetingTypeName NVARCHAR(100) NOT NULL UNIQUE,
    Remarks NVARCHAR(100) NOT NULL,
    Created DATETIME DEFAULT GETDATE(),
    Modified DATETIME NOT NULL
);
--INSERT--
CREATE PROCEDURE SP_Insert_MOM_MeetingType
    @MeetingTypeName NVARCHAR(100),
    @Remarks NVARCHAR(100)
AS
BEGIN
    INSERT INTO MOM_MeetingType
    (MeetingTypeName, Remarks, Created, Modified)
    VALUES
    (@MeetingTypeName, @Remarks, GETDATE(), GETDATE())
END
EXEC SP_Insert_MOM_MeetingType
    @MeetingTypeName = 'Review Meeting',
    @Remarks = 'Monthly review'
EXEC SP_Insert_MOM_MeetingType
    @MeetingTypeName = 'Meeting',
    @Remarks = 'yearly review'
EXEC SP_Insert_MOM_MeetingType
    @MeetingTypeName = 'weekly Meeting',
    @Remarks = 'weekly review'
EXEC SP_Insert_MOM_MeetingType
    @MeetingTypeName = 'daily Meeting',
    @Remarks = 'daily review'
EXEC SP_Insert_MOM_MeetingType 
    @MeetingTypeName = 'Client Meeting',
    @Remarks = 'Discussion with client';
EXEC SP_Insert_MOM_MeetingType 
    @MeetingTypeName = 'yearly Meeting',
    @Remarks = 'Discussion with Faculty';

select * from MOM_MeetingType

--UPDATE--
CREATE PROCEDURE SP_Update_MOM_MeetingType
    @MeetingTypeID INT,
    @MeetingTypeName NVARCHAR(100),
    @Remarks NVARCHAR(100)
AS
BEGIN
    UPDATE MOM_MeetingType
    SET MeetingTypeName = @MeetingTypeName,
        Remarks = @Remarks,
        Modified = GETDATE()
    WHERE MeetingTypeID = @MeetingTypeID
END
EXEC SP_Update_MOM_MeetingType
    @MeetingTypeID = 1,
    @MeetingTypeName = 'Planning Meeting',
    @Remarks = 'Project planning'

--DELETE--
CREATE PROCEDURE SP_Delete_MOM_MeetingType
    @MeetingTypeID INT
AS
BEGIN
    DELETE FROM MOM_MeetingType
    WHERE MeetingTypeID = @MeetingTypeID
END
EXEC SP_Delete_MOM_MeetingType
    @MeetingTypeID = 3

--Select All--
CREATE or alter PROCEDURE SP_SelectAll_MOM_MeetingType
AS
BEGIN
    SELECT MeetingTypeID,MeetingTypeName,Remarks,Created,Modified FROM MOM_MeetingType
END

--SelectByID--
CREATE PROCEDURE SP_SelectByID_MOM_MeetingType
    @MeetingTypeID INT
AS
BEGIN
    SELECT * FROM MOM_MeetingType
    WHERE MeetingTypeID = @MeetingTypeID
END
EXEC SP_SelectByID_MOM_MeetingType
    @MeetingTypeID = 1


--MOM_MeetingVenue--
CREATE TABLE MOM_MeetingVenue (
    MeetingVenueID INT IDENTITY(1,1) PRIMARY KEY,
    MeetingVenueName NVARCHAR(100) NOT NULL UNIQUE,
    Created DATETIME DEFAULT GETDATE(),
    Modified DATETIME NOT NULL
);
--INSERT--
CREATE PROCEDURE SP_Insert_MOM_MeetingVenue
    @MeetingVenueName NVARCHAR(100)
AS
BEGIN
    INSERT INTO MOM_MeetingVenue (MeetingVenueName, Created, Modified)
    VALUES (@MeetingVenueName, GETDATE(), GETDATE())
END
EXEC SP_Insert_MOM_MeetingVenue 
    @MeetingVenueName = 'Main Conference Hall'
EXEC SP_Insert_MOM_MeetingVenue 
    @MeetingVenueName = 'KKV Hall'
EXEC SP_Insert_MOM_MeetingVenue 
    @MeetingVenueName = 'Jalkanyan Hall'
EXEC SP_Insert_MOM_MeetingVenue 
    @MeetingVenueName = 'Cristal Hall'
EXEC SP_Insert_MOM_MeetingVenue 
    @MeetingVenueName = 'Conference Hall A'
EXEC SP_Insert_MOM_MeetingVenue 
    @MeetingVenueName = 'Conference Hall B';

select * from MOM_MeetingVenue
--UPDATE--
CREATE PROCEDURE SP_Update_MOM_MeetingVenue
    @MeetingVenueID INT,
    @MeetingVenueName NVARCHAR(100)
AS
BEGIN
    UPDATE MOM_MeetingVenue
    SET MeetingVenueName = @MeetingVenueName,
        Modified = GETDATE()
    WHERE MeetingVenueID = @MeetingVenueID
END
EXEC SP_Update_MOM_MeetingVenue 
    @MeetingVenueID = 1,
    @MeetingVenueName = 'Board Room'

--DELETE--
CREATE PROCEDURE SP_Delete_MOM_MeetingVenue
    @MeetingVenueID INT
AS
BEGIN
    DELETE FROM MOM_MeetingVenue
    WHERE MeetingVenueID = @MeetingVenueID
END
EXEC SP_Delete_MOM_MeetingVenue 
    @MeetingVenueID = 2

--SELECTALL--
CREATE PROCEDURE SP_SelectAll_MOM_MeetingVenue
AS
BEGIN
   SELECT
    MeetingVenueID,
    MeetingVenueName,
    Created,
    Modified
FROM MOM_MeetingVenue


--SELECTBYID--
CREATE or alter PROCEDURE SP_SelectByID_MOM_MeetingVenue
    @MeetingVenueID INT
AS
BEGIN
    SELECT
    MeetingVenueID,
    MeetingVenueName,
    Created,
    Modified
FROM MOM_MeetingVenue

    WHERE MeetingVenueID = @MeetingVenueID
END
EXEC SP_SelectByID_MOM_MeetingVenue 
    @MeetingVenueID = 1

--MOM_Staff TABLE--
CREATE TABLE MOM_Staff (
    StaffID INT IDENTITY(1,1) PRIMARY KEY,
    DepartmentID INT NOT NULL,
    StaffName NVARCHAR(50) NOT NULL,
    MobileNo NVARCHAR(20) NOT NULL,
    EmailAddress NVARCHAR(50) NOT NULL UNIQUE,
    Remarks NVARCHAR(250),
    Created DATETIME DEFAULT GETDATE(),
    Modified DATETIME NOT NULL,
    FOREIGN KEY (DepartmentID) REFERENCES MOM_Department(DepartmentID)
);
-- 1. SelectAll Procedure for MOM_Staff
CREATE OR ALTER PROCEDURE [dbo].[PR_Staff_SelectAll]
AS
BEGIN
    SELECT s.StaffID,
           s.DepartmentID,
           d.DepartmentName,
           s.StaffName,
           s.MobileNo,
           s.EmailAddress,
           s.Remarks,
           s.Created,
           s.Modified
    FROM [dbo].[MOM_Staff] s
    INNER JOIN [dbo].[MOM_Department] d ON s.DepartmentID = d.DepartmentID
    ORDER BY s.StaffName
END
EXEC dbo.PR_Staff_SelectAll;


-- 2. SelectByPK Procedure for MOM_Staff
CREATE OR ALTER PROCEDURE [dbo].[PR_Staff_SelectByPK]
@StaffID INT
AS
BEGIN
    SELECT s.StaffID,
           s.DepartmentID,
           d.DepartmentName,
           s.StaffName,
           s.MobileNo,
           s.EmailAddress,
           s.Remarks,
           s.Created,
           s.Modified
    FROM [dbo].[MOM_Staff] s
    INNER JOIN [dbo].[MOM_Department] d ON s.DepartmentID = d.DepartmentID
    WHERE s.StaffID = @StaffID
END
EXEC PR_Staff_SelectByPK 5;



-- 3. Insert Procedure for MOM_Staff
CREATE OR ALTER PROCEDURE [dbo].[PR_Staff_Insert]
@DepartmentID INT,
@StaffName    NVARCHAR(50),
@MobileNo     NVARCHAR(20),
@EmailAddress NVARCHAR(50),
@Remarks      NVARCHAR(250)
AS
BEGIN
    INSERT INTO [dbo].[MOM_Staff]
    (
        DepartmentID,
        StaffName,
        MobileNo,
        EmailAddress,
        Remarks,
        Modified
    )
    VALUES
    (
        @DepartmentID,
        @StaffName,
        @MobileNo,
        @EmailAddress,
        @Remarks,
        GETDATE()
    )
END
EXEC PR_Staff_Insert 1, 'Amit Patel', '9876543210', 'amit.patel@gmail.com', 'IT Department Staff';
EXEC PR_Staff_Insert 2, 'riya Patel', '9873543210', 'riya.patel@gmail.com', 'CE Department Staff';
EXEC PR_Staff_Insert 3, 'aditi Patel', '9876543210', 'aditi.patel@gmail.com', 'BBA Department Staff';
EXEC PR_Staff_Insert 4, 'kajal Patel', '9876543210', 'kajal.patel@gmail.com', 'BCA Department Staff';
EXEC PR_Staff_Insert 14, 'jeel patel',  '9988776655', 'jeel@company.com', 'Project Manager';

select *from MOM_Staff

--- 4. Update Procedure for MOM_Staff
CREATE OR ALTER PROCEDURE [dbo].[PR_Staff_UpdateByPK]
@StaffID      INT,
@DepartmentID INT,
@StaffName    NVARCHAR(50),
@MobileNo     NVARCHAR(20),
@EmailAddress NVARCHAR(50),
@Remarks      NVARCHAR(250)
AS
BEGIN
    UPDATE [dbo].[MOM_Staff]
    SET
        DepartmentID = @DepartmentID,
        StaffName = @StaffName,
        MobileNo = @MobileNo,
        EmailAddress = @EmailAddress,
        Remarks = @Remarks,
        Modified = GETDATE()
    WHERE StaffID = @StaffID
END
EXEC PR_Staff_UpdateByPK 7, 2, 'Amit Patel', '9998887776', 'amit.patel@company.com', 'Updated department and contact details';


-- 5. Delete Procedure for MOM_Staff
CREATE OR ALTER PROCEDURE [dbo].[PR_Staff_DeleteByPK]
@StaffID INT
AS
BEGIN
    DELETE FROM [dbo].[MOM_Staff]
    WHERE StaffID = @StaffID
END
GO
EXEC dbo.PR_Staff_DeleteByPK @StaffID = 11;


--MOM_Meetings--
CREATE TABLE MOM_Meetings (
    MeetingID INT IDENTITY(1,1) PRIMARY KEY,
    MeetingDate DATETIME NOT NULL,
    MeetingVenueID INT NOT NULL,
    MeetingTypeID INT NOT NULL,
    DepartmentID INT NOT NULL,
    MeetingDescription NVARCHAR(250),
    DocumentPath NVARCHAR(250),
    Created DATETIME DEFAULT GETDATE(),
    Modified DATETIME NOT NULL,
    IsCancelled BIT,
    CancellationDateTime DATETIME,
    CancellationReason NVARCHAR(250),
    FOREIGN KEY (MeetingVenueID) REFERENCES MOM_MeetingVenue(MeetingVenueID),
    FOREIGN KEY (MeetingTypeID) REFERENCES MOM_MeetingType(MeetingTypeID),
    FOREIGN KEY (DepartmentID) REFERENCES MOM_Department(DepartmentID)
);
-- 1. SelectAll Procedure for MOM_Meetings
CREATE OR ALTER PROCEDURE [dbo].[PR_Meetings_SelectAll]
AS
BEGIN
    SELECT m.MeetingID,
           m.MeetingDate,
           m.MeetingVenueID,
           mv.MeetingVenueName,
           m.MeetingTypeID,
           mt.MeetingTypeName,
           m.DepartmentID,
           d.DepartmentName,
           m.MeetingDescription,
           m.DocumentPath,
           m.Created,
           m.Modified,
           m.IsCancelled,
           m.CancellationDateTime,
           m.CancellationReason
    FROM [dbo].[MOM_Meetings] m
    INNER JOIN [dbo].[MOM_MeetingVenue] mv ON m.MeetingVenueID = mv.MeetingVenueID
    INNER JOIN [dbo].[MOM_MeetingType] mt ON m.MeetingTypeID = mt.MeetingTypeID
    INNER JOIN [dbo].[MOM_Department] d ON m.DepartmentID = d.DepartmentID
    ORDER BY m.MeetingDate DESC
END

-- 2. SelectByPK Procedure for MOM_Meetings
CREATE OR ALTER PROCEDURE [dbo].[PR_Meetings_SelectByPK]
@MeetingID INT
AS
BEGIN
    SELECT m.MeetingID,
           m.MeetingDate,
           m.MeetingVenueID,
           mv.MeetingVenueName,
           m.MeetingTypeID,
           mt.MeetingTypeName,
           m.DepartmentID,
           d.DepartmentName,
           m.MeetingDescription,
           m.DocumentPath,
           m.Created,
           m.Modified,
           m.IsCancelled,
           m.CancellationDateTime,
           m.CancellationReason
    FROM [dbo].[MOM_Meetings] m
    INNER JOIN [dbo].[MOM_MeetingVenue] mv ON m.MeetingVenueID = mv.MeetingVenueID
    INNER JOIN [dbo].[MOM_MeetingType] mt ON m.MeetingTypeID = mt.MeetingTypeID
    INNER JOIN [dbo].[MOM_Department] d ON m.DepartmentID = d.DepartmentID
    WHERE m.MeetingID = @MeetingID
END
EXEC dbo.PR_Meetings_SelectByPK @MeetingID = 3;

-- 3. Insert Procedure for MOM_Meetings
CREATE OR ALTER PROCEDURE [dbo].[PR_Meetings_Insert]
@MeetingDate        DATETIME,
@MeetingVenueID     INT,
@MeetingTypeID      INT,
@DepartmentID       INT,
@MeetingDescription NVARCHAR(250),
@DocumentPath       NVARCHAR(250)
AS
BEGIN
    INSERT INTO [dbo].[MOM_Meetings]
    (
        MeetingDate,
        MeetingVenueID,
        MeetingTypeID,
        DepartmentID,
        MeetingDescription,
        DocumentPath,
        Modified
    )
    VALUES
    (
        @MeetingDate,
        @MeetingVenueID,
        @MeetingTypeID,
        @DepartmentID,
        @MeetingDescription,
        @DocumentPath,
        GETDATE()
    )
END
EXEC dbo.PR_Meetings_Insert
    @MeetingDate        = '2025-12-21 09:00:00',
    @MeetingVenueID     = 1,
    @MeetingTypeID      = 1,
    @DepartmentID       = 1,
    @MeetingDescription = 'Project kickoff meeting',
    @DocumentPath       = 'C:\Documents\MoM\kickoff.pdf';

EXEC dbo.PR_Meetings_Insert
    @MeetingDate        = '2025-12-22 10:30:00',
    @MeetingVenueID     = 1,
    @MeetingTypeID      = 1,
    @DepartmentID       = 1,
    @MeetingDescription = 'Requirement discussion meeting',
    @DocumentPath       = 'C:\Documents\MoM\requirements.pdf';

EXEC dbo.PR_Meetings_Insert
    @MeetingDate        = '2025-12-23 11:00:00',
    @MeetingVenueID     = 1,
    @MeetingTypeID      = 1,
    @DepartmentID       = 1,
    @MeetingDescription = 'Design review meeting',
    @DocumentPath       = 'C:\Documents\MoM\design_review.pdf';

EXEC dbo.PR_Meetings_Insert
    @MeetingDate        = '2025-12-24 02:00:00',
    @MeetingVenueID     = 3,
    @MeetingTypeID      = 10,
    @DepartmentID       = 14,
    @MeetingDescription = 'Sprint planning meeting',
    @DocumentPath       = 'C:\Documents\MoM\sprint_planning.pdf';

EXEC dbo.PR_Meetings_Insert
    @MeetingDate        = '2025-12-25 04:00:00',
    @MeetingVenueID     = 1,
    @MeetingTypeID      = 1,
    @DepartmentID       = 1,
    @MeetingDescription = 'Weekly review meeting',
    @DocumentPath       = 'C:\Documents\MoM\weekly_review.pdf';

select * from MOM_Meetings

-- 4. Update Procedure for MOM_Meetings
CREATE OR ALTER PROCEDURE [dbo].[PR_Meetings_UpdateByPK]
@MeetingID          INT,
@MeetingDate        DATETIME,
@MeetingVenueID     INT,
@MeetingTypeID      INT,
@DepartmentID       INT,
@MeetingDescription NVARCHAR(250),
@DocumentPath       NVARCHAR(250)
AS
BEGIN
    UPDATE [dbo].[MOM_Meetings]
    SET
        MeetingDate = @MeetingDate,
        MeetingVenueID = @MeetingVenueID,
        MeetingTypeID = @MeetingTypeID,
        DepartmentID = @DepartmentID,
        MeetingDescription = @MeetingDescription,
        DocumentPath = @DocumentPath,
        Modified = GETDATE()
    WHERE MeetingID = @MeetingID
END
EXEC dbo.PR_Meetings_UpdateByPK
    @MeetingID          = 4,
    @MeetingDate        = '2025-12-26 10:00:00',
    @MeetingVenueID     = 4,
    @MeetingTypeID      = 5,
    @DepartmentID       = 20,
    @MeetingDescription = 'Updated weekly review meeting',
    @DocumentPath       = 'C:\Documents\MoM\weekly_review_updated.pdf';

-- 5. Delete Procedure for MOM_Meetings
CREATE OR ALTER PROCEDURE [dbo].[PR_Meetings_DeleteByPK]
@MeetingID INT
AS
BEGIN
    DELETE FROM [dbo].[MOM_Meetings]
    WHERE MeetingID = @MeetingID
END
EXEC dbo.PR_Meetings_DeleteByPK @MeetingID = 5;



---MOM_MeetingMember--
CREATE TABLE MOM_MeetingMember (
    MeetingMemberID INT IDENTITY(1,1) PRIMARY KEY,
    MeetingID INT NOT NULL,
    StaffID INT NOT NULL,
    IsPresent BIT NOT NULL,
    Remarks NVARCHAR(250),
    Created DATETIME DEFAULT GETDATE(),
    Modified DATETIME NOT NULL,
    FOREIGN KEY (MeetingID) REFERENCES MOM_Meetings(MeetingID),
    FOREIGN KEY (StaffID) REFERENCES MOM_Staff(StaffID),
    CONSTRAINT UQ_MeetingStaff UNIQUE (MeetingID, StaffID)
);
-- 1. SelectAll Procedure for MOM_MeetingMember
CREATE OR ALTER PROCEDURE [dbo].[PR_MeetingMember_SelectAll]
AS
BEGIN  
    SELECT 
        mm.MeetingMemberID,
        mm.MeetingID,
        m. MeetingDescription,   
        mm.StaffID,
        s.StaffName,
        d.DepartmentName,                     
        mm.IsPresent,
        mm.Remarks
    FROM MOM_MeetingMember mm
    INNER JOIN MOM_Meetings m ON mm.MeetingID = m.MeetingID
    INNER JOIN MOM_Staff s ON mm.StaffID = s.StaffID
    INNER JOIN MOM_Department d ON s.DepartmentID = d.DepartmentID
    ORDER BY m.MeetingDescription, s.StaffName
END
GO


-- 2. SelectByPK Procedure for MOM_MeetingMember
CREATE OR ALTER PROCEDURE [dbo].[PR_MeetingMember_SelectByPK]
@MeetingMemberID INT
AS
BEGIN
    SELECT mm.MeetingMemberID,
           mm.MeetingID,
           m.MeetingDate,
           mm.StaffID,
           s.StaffName,
           s.EmailAddress,
           mm.IsPresent,
           mm.Remarks,
           mm.Created,
           mm.Modified
    FROM [dbo].[MOM_MeetingMember] mm
    INNER JOIN [dbo].[MOM_Meetings] m ON mm.MeetingID = m.MeetingID
    INNER JOIN [dbo].[MOM_Staff] s ON mm.StaffID = s.StaffID
    WHERE mm.MeetingMemberID = @MeetingMemberID
END
EXEC dbo.PR_MeetingMember_SelectByPK @MeetingMemberID = 5;


-- 3. Insert Procedure for MOM_MeetingMember
CREATE OR ALTER PROCEDURE [dbo].[PR_MeetingMember_Insert]
@MeetingID INT,
@StaffID   INT,
@IsPresent BIT,
@Remarks   NVARCHAR(250)
AS
BEGIN
    INSERT INTO [dbo].[MOM_MeetingMember]
    (
        MeetingID,
        StaffID,
        IsPresent,
        Remarks,
        Modified
    )
    VALUES
    (
        @MeetingID,
        @StaffID,
        @IsPresent,
        @Remarks,
        GETDATE()
    )
END
EXEC dbo.PR_MeetingMember_Insert
    @MeetingID = 3,
    @StaffID   = 7,
    @IsPresent = 1,
    @Remarks   = 'Present in meeting';


EXEC dbo.PR_MeetingMember_Insert
    @MeetingID = 4,
    @StaffID   = 9,
    @IsPresent = 1,
    @Remarks   = 'Attended and participated';

EXEC dbo.PR_MeetingMember_Insert
    @MeetingID = 6,
    @StaffID   = 19,
    @IsPresent = 1,
    @Remarks   = 'Attended and participated';


EXEC dbo.PR_MeetingMember_Insert
    @MeetingID = 4,
    @StaffID   = 16,
    @IsPresent = 2,
    @Remarks   = 'Attended in class';

-- 4. Update Procedure for MOM_MeetingMember
CREATE OR ALTER PROCEDURE [dbo].[PR_MeetingMember_UpdateByPK]
@MeetingMemberID INT,
@MeetingID       INT,
@StaffID         INT,
@IsPresent       BIT,
@Remarks         NVARCHAR(250)
AS
BEGIN
    UPDATE [dbo].[MOM_MeetingMember]
    SET
        MeetingID = @MeetingID,
        StaffID = @StaffID,
        IsPresent = @IsPresent,
        Remarks = @Remarks,
        Modified = GETDATE()
    WHERE MeetingMemberID = @MeetingMemberID
END
EXEC dbo.PR_MeetingMember_UpdateByPK
    @MeetingMemberID = 5,
    @MeetingID       = 3,
    @StaffID         = 7,
    @IsPresent       = 1,
    @Remarks         = 'Updated: confirmed presence';

-- 5. Delete Procedure for MOM_MeetingMember
CREATE OR ALTER PROCEDURE [dbo].[PR_MeetingMember_DeleteByPK]
@MeetingMemberID INT
AS
BEGIN
    DELETE FROM [dbo].[MOM_MeetingMember]
    WHERE MeetingMemberID = @MeetingMemberID
END


select * from MOM_Department
select * from MOM_MeetingMember
select * from MOM_Meetings
select * from MOM_MeetingType
select * from MOM_MeetingVenue
select * from MOM_Staff




