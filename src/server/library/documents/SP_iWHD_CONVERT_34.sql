
IF EXISTS (SELECT * FROM sysobjects WHERE id=object_id(N'SP_iWHD_UPGRADE34') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP procedure SP_iWHD_UPGRADE34
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

/*---------------------------------------------------------------------------------------------------*/
/*
	EXEC SP_iWHD_UPGRADE34 4, 16, 'demo'
	
	-- 데이터 이전을 해도 guid가 잘못 이전됐기 때문에 아래의 업데이트 문장 2개를 실행해줘야 합니다.
	-- TB_iWHD_DIRECTORY guid 업데이트
	UPDATE TB_iWHD_DIRECTORY SET
	guid = (SELECT Replace(vname, '.bin', '') FROM TB_iWHD_MASTER 
	WHERE ftype = 'F' AND companyid = '1001' AND A.guid = guid)
	FROM TB_iWHD_DIRECTORY A
	WHERE A.ftype = 'F'

	-- TB_iWHD_AUTHORITY guid 업데이트
	UPDATE TB_iWHD_AUTHORITY SET
	guid = Replace(B.vname, '.bin', '')
	--SELECT * 
	FROM TB_iWHD_AUTHORITY A INNER JOIN TB_iWHD_MASTER B
	ON A.guid = B.guid INNER JOIN TB_iWHD_DIRECTORY C
	ON Replace(B.vname, '.bin', '') = C.guid
	WHERE C.ftype = 'F'




	SELECT * FROM TB_iTMP_MASTER
		WHERE ISNULL(fileid,'')!=''
		ORDER BY fileid

	SELECT * FROM TB_iTMP_MASTER
		WHERE level <= 4 
		ORDER BY fileid
*/
/*---------------------------------------------------------------------------------------------------*/
CREATE PROCEDURE SP_iWHD_UPGRADE34
(
	@psLength		decimal(10) = 4,
	@psLevel		decimal(10) = 0,
	@psCompanyID	varchar(32) = 'demo'
)
AS
BEGIN
	SET NOCOUNT ON;

    IF EXISTS (SELECT * FROM sysobjects WHERE name = N'TB_iTMP_MASTER') DROP TABLE TB_iTMP_MASTER

    CREATE TABLE TB_iTMP_MASTER
    (
        guid			nvarchar(64)		not null default(newid()),		-- Guid Unique Key(UniqueID) 

	    companyid		nvarchar(64)		not null default(''),			-- Company's Identity Key

	    fileid          nvarchar(256)		not null default(''),			-- serial file number (256 / 4 = 64Level)

	    level           decimal(5)          not null default(0),            -- position(depth) of tree structure(TreeLevel)
	    unode           decimal(5)          not null default(0),            -- parent node number(UpperNode)
	    cnode           decimal(5)          not null default(0),            -- current node number(Node)
	    rnode           decimal(5)          not null default(0),            -- top of the tree node number for cnode(RootNode)

	    ftype           nvarchar(1)         not null default('F'),          -- type-of-record(T:Folder, F:File) (RnQns)
	
    	vname           nvarchar(256)       not null default(''),           -- virtual file name(FileName)
	    vsize           decimal(10)         not null default(0),            -- size of file(FileSize)
    	vtype           nvarchar(256)       not null default(''),           -- type of file(FileType)
	
	    rname           nvarchar(256)       not null default(''),           -- known to user's real file name(RealFileName)
	    title           nvarchar(256)       not null default(''),           -- title of real file(Title)
	    description     ntext               not null default(''),           -- description of file(Contents)

	    wcode           nvarchar(256)       not null default(''),           -- writer's code(WriterID)
	    wname           nvarchar(256)       not null default(''),           -- writer's name(WriterName)
	    wdate           datetime            not null default(getdate()),	-- date of write(WriteDate)

    	sfid			datetime			not null default(getdate()),	-- first insert time
    	slmd			datetime			not null default(getdate())		-- last update time
    )
    
    INSERT INTO TB_iTMP_MASTER
    SELECT guid,companyid,'',level,unode,cnode,rnode,ftype,vname,vsize,vtype,rname,title,description,wcode,wname,wdate,sfid,slmd
    FROM TB_iWHD_MASTER
	WHERE companyid=@psCompanyID
    
	/*------------------------------------------------------------------------------------------------*/
	--
	/*------------------------------------------------------------------------------------------------*/
	DECLARE @fileid nvarchar(128), @cnode decimal(10), @rnode decimal(10), @level decimal(10),
			@filecd nvarchar(128), @guid nvarchar(64), @seqno decimal(10), @nextcd nvarchar(128),
			@percent decimal(10,2), @count decimal(10), @position decimal(10), @progress decimal(10,2)

	SELECT @count = COUNT(*)
		FROM TB_iTMP_MASTER
		WHERE level<=@psLevel AND ftype='T'

	PRINT @count

	DECLARE CUR_iWHD_MASTER CURSOR FOR 
	SELECT rnode, level, cnode
		FROM TB_iTMP_MASTER
		WHERE level<=@psLevel AND ftype='T'
		ORDER BY rnode, level, cnode

	OPEN CUR_iWHD_MASTER
	FETCH NEXT FROM CUR_iWHD_MASTER INTO @rnode, @level, @cnode

	SET @position = 0;
	SET @progress = 0;

	WHILE @@FETCH_STATUS = 0
	BEGIN
		IF (@level = 0)
		BEGIN
			SELECT @fileid = REPLICATE('0', @psLength-LEN(CONVERT(nvarchar,cnode))) + CONVERT(nvarchar,cnode)
				FROM TB_iTMP_MASTER
				WHERE rnode=@rnode AND level=@level AND cnode=@cnode AND ftype='T'

			UPDATE TB_iTMP_MASTER
				SET fileid=@fileid
				WHERE rnode=@rnode AND level=@level AND cnode=@cnode AND ftype='T'

		END ELSE
		BEGIN
			SELECT @fileid=fileid
				FROM TB_iTMP_MASTER
				WHERE rnode=@rnode AND level=@level AND cnode=@cnode AND ftype='T'
		END

		DECLARE CUR_iWHD_SLAVE CURSOR FOR 
			SELECT guid, CONVERT(nvarchar, cnode) as cnode
			FROM TB_iTMP_MASTER
			WHERE level=@level+1 AND unode=@cnode AND rnode=@rnode --AND ftype='T'
			ORDER BY cnode

		OPEN CUR_iWHD_SLAVE
		FETCH NEXT FROM CUR_iWHD_SLAVE INTO @guid, @filecd

		SET @seqno = 1

		WHILE @@FETCH_STATUS = 0
		BEGIN
			SET @nextcd = @fileid + REPLICATE('0', @psLength-LEN(CONVERT(nvarchar,@seqno))) + CONVERT(nvarchar,@seqno)

			--PRINT @nextcd
			UPDATE TB_iTMP_MASTER 
			SET fileid=@nextcd
			WHERE guid=@guid
	
			SET @seqno = @seqno + 1

			FETCH NEXT FROM CUR_iWHD_SLAVE INTO @guid, @filecd	
		END
		
		CLOSE CUR_iWHD_SLAVE
		DEALLOCATE CUR_iWHD_SLAVE

		SET @position = @position + 1;
		SET @percent = CONVERT(decimal(10,2), @position / @count * 100.0)
	
		IF (@progress + 5.0 < @percent)
		BEGIN
			SET @progress = @percent
			PRINT CONVERT(nvarchar, @percent) + '(%)'
		END

		FETCH NEXT FROM CUR_iWHD_MASTER INTO @rnode, @level, @cnode
	END

	CLOSE CUR_iWHD_MASTER
	DEALLOCATE CUR_iWHD_MASTER

	/*------------------------------------------------------------------------------------------------*/
	--
	/*------------------------------------------------------------------------------------------------*/
    IF EXISTS (SELECT * FROM sysobjects WHERE name = N'TB_iWHD_DIRECTORY') DROP TABLE TB_iWHD_DIRECTORY
    CREATE TABLE TB_iWHD_DIRECTORY
    (
        guid			nvarchar(64)		not null default(newid()),		-- Guid Unique Key(UniqueID) 

	    companyid		nvarchar(64)		not null default(''),			-- Company's Identity Key

	    fileid          nvarchar(256)		not null default(''),			-- serial file number (256 / 4 = 64Level)
	    cnode           decimal(5)          not null default(0),            -- current node number(Node)

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

    ALTER TABLE TB_iWHD_DIRECTORY ADD PRIMARY KEY(guid);
    CREATE UNIQUE INDEX IX_iWHD_DIRECTORY ON TB_iWHD_DIRECTORY(companyid, fileid);

    IF EXISTS (SELECT * FROM sysobjects WHERE name = N'TB_iWHD_AUTHORITY') DROP TABLE TB_iWHD_AUTHORITY
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

    ALTER TABLE TB_iWHD_AUTHORITY ADD PRIMARY KEY(guid, member, mtype);

    INSERT INTO TB_iWHD_DIRECTORY
    SELECT guid,companyid,fileid,cnode,ftype,vsize,vtype,rname,title,description,'',wcode,wname,wdate,sfid,slmd
    FROM TB_iTMP_MASTER

    INSERT INTO TB_iWHD_AUTHORITY
    SELECT guid,member,mtype,name,control,cmodify,cread,cdelete,cview,cfolder,cfile,sfid,slmd
    FROM TB_iWHD_AUTHLIST

    SET NOCOUNT OFF

	/*------------------------------------------------------------------------------------------------*/
	--
	/*------------------------------------------------------------------------------------------------*/
END;