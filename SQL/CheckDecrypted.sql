SELECT 
    UserId, 
    UserName, 
    Email, 
	UserPassword AS EncryptedPassword,
    CONVERT(VARCHAR, DECRYPTBYPASSPHRASE('MySecretKey', UserPassword)) AS DecryptedPassword 
FROM Users;