
CREATE LOGIN customer_user WITH PASSWORD = 'customer123';

-- Create a User in the ChatRoomDB database for the Readonly User
CREATE USER customer_user FOR LOGIN customer_user;

-- Grant Read-only Access (SELECT permission) on the Users table
GRANT SELECT ON Users TO customer_user;

-- Create a Login for the Owner User
CREATE LOGIN owner_user WITH PASSWORD = 'owner123';

CREATE USER owner_user FOR LOGIN owner_user;

-- Grant the Owner User db_owner role (full access to all objects in the database)
ALTER ROLE db_owner ADD MEMBER owner_user;