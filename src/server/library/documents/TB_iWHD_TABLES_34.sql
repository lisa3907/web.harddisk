/*------------------------------------------------------------------------------------------------*/
-- Title 	: '웹하드 목록 관리 테이블'
-- Author 	: '이성안'
-- Date 	: 2007/09/11
-- NickName : TB_iWHD_DIRECTORY
-- Version 	: V3.3.2007.09
/*------------------------------------------------------------------------------------------------*/
IF EXISTS (SELECT * FROM sysobjects WHERE name = N'TB_iWHD_DIRECTORY') DROP TABLE TB_iWHD_DIRECTORY
Go

CREATE TABLE TB_iWHD_DIRECTORY
(
    guid			nvarchar(64)		not null default(newid()),		-- Guid Unique Key(UniqueID) 

	companyid		nvarchar(64)		not null default(''),			-- Company's Identity Key

	fileid          nvarchar(256)		not null default(''),			-- serial file number (256 / 4 = 64Level)
	--cnode           decimal(5)          not null default(0),            -- current node number(Node)

	ftype           nvarchar(1)         not null default('F'),          -- type-of-record(T:Folder, F:File) (RnQns)
	vsize           decimal(10)         not null default(0),            -- size of file(FileSize)
	vtype           nvarchar(256)       not null default(''),           -- type of file(FileType)
	
	rname           nvarchar(256)       not null default(''),           -- known to user's real file name(RealFileName)
	title           nvarchar(256)       not null default(''),           -- title of real file(Title)
	description     ntext               not null default(''),           -- description of file(Contents)

	attach          xml                     null,                       -- attach file stream
	
	wcode           nvarchar(256)       not null default(''),           -- writer's code(WriterID)
	wname           nvarchar(256)       not null default(''),           -- writer's name(WriterName)
	wdate           datetime            not null default(getdate()),	-- date of write(WriteDate)

	sfid			datetime			not null default(getdate()),	-- first insert time
	slmd			datetime			not null default(getdate())		-- last update time
)
Go

ALTER TABLE TB_iWHD_DIRECTORY ADD PRIMARY KEY(guid);
Go

CREATE UNIQUE INDEX IX_iWHD_DIRECTORY ON TB_iWHD_DIRECTORY(companyid, fileid);
Go

/*------------------------------------------------------------------------------------------------
OLD: guid,companyid,level,unode,cnode,rnode,ftype,vname,vsize,vtype,rname,title,description,wcode,wname,wdate,sfid,slmd
NEW: guid,companyid,            cnode,      ftype,      vsize,vtype,rname,title,description,wcode,wname,wdate,sfid,slmd,fileid,attach

------------------------------------------------------------------------------------------------*/

/*------------------------------------------------------------------------------------------------*/
-- Title 	: '웹하드 권한 관리 테이블'
-- Author 	: '이성안'
-- Date 	: 2007/09/11
-- NickName : TB_iWHD_AUTHORITY
-- Version 	: V3.3.2007.09
/*------------------------------------------------------------------------------------------------*/
IF EXISTS (SELECT * FROM sysobjects WHERE name = N'TB_iWHD_AUTHORITY') DROP TABLE TB_iWHD_AUTHORITY
Go

CREATE TABLE TB_iWHD_AUTHORITY
(
    guid			nvarchar(64)		not null,		                -- Guid Unique Key of Master(UniqueID) 

	member          nvarchar(256)       not null default(''),           -- group or person id(code)	
	mtype           nvarchar(1)         not null default('F'),          -- type-of-record(T:Group, F:Person)
	name            nvarchar(256)       not null default(''),           -- group or person's name(Realname)
	
    control         nvarchar(1)         not null default('F'),          -- can control authority(hAuthModify)
	cmodify         nvarchar(1)         not null default('F'),          -- can modify file or group(hModify)
	cread           nvarchar(1)         not null default('F'),          -- can read file or group(hRead)
	cdelete         nvarchar(1)         not null default('F'),          -- can delete file or group(hDelete)
	cview           nvarchar(1)         not null default('F'),          -- can view file or group(hView)
	cfolder         nvarchar(1)         not null default('F'),          -- can create group(hFolderCreate)
	cfile           nvarchar(1)         not null default('F'),          -- can create file(hFileCreate)
	
	sfid			datetime			not null default(getdate()),	-- first insert time
	slmd			datetime			not null default(getdate())		-- last update time
)
Go

ALTER TABLE TB_iWHD_AUTHORITY ADD PRIMARY KEY(guid, member, mtype);
Go

/*------------------------------------------------------------------------------------------------
OLD: guid,ftype,member,mtype,name,control,cmodify,cread,cdelete,cview,cfolder,cfile,sfid,slmd
NEW: guid,      member,mtype,name,control,cmodify,cread,cdelete,cview,cfolder,cfile,sfid,slmd

EXEC SP_iWHD_UPGRADE34 4, 16
------------------------------------------------------------------------------------------------*/
