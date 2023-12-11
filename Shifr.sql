
UPDATE UsersTable
	SET Password = CONVERT(Varchar(64),
	HASHBYTES('MD5',Password),2)

	UPDATE UsersTable
	SET Login = CONVERT(Varchar(64),
	HASHBYTES('MD5',Login),2)

SELECT * FROM UsersTable