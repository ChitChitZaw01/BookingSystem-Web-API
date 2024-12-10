Code-First approach in Entity Framework (EF)
>> All users' passwords are '123'

To install packages
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package StackExchange.Redis
dotnet add package Hangfire
dotnet add package Swashbuckle.AspNetCore

To check my computer amd64 or arm64
Press Windows + R to open the Run dialog.
Type msinfo32 and press Enter to open the System Information window.
In the System Information window, look for the entry called System Type.
If it says x64-based PC, your system is amd64 (x86_64).
If it says ARM-based PC, your system is arm64.


To run for database and seed data
>> dotnet ef migrations add MigrationName
>> dotnet ef database update

For installing using Redis on docker desktop
1. Download Docker Desktop
2. Install Docker Desktop
3. Restart Your Computer
4. Verify Docker Installation in docker terminal, so run the following command in docker terminal
	docker --version   
5. Pull Redis Image from Docker Hub, so run the following command in docker terminal
	docker pull redis
6. We will run Redis inside a Docker container, so run the following command in docker terminal
	docker run --name redis -p 6379:6379 -d redis
	Here's what each part of the command means:
--name redis: This names the container as "redis" (you can choose any name).
-p 6379:6379: This maps port 6379 on your machine to port 6379 in the container (the default Redis port).
-d: This runs the container in the background.
redis: This is the name of the image that Docker will use to create the container.
7. Verify Redis is Running, so run the following command in docker terminal
	docker ps
	You should see the Redis container listed with the port 6379 exposed
8. Access Redis from Docker
	You can access the Redis CLI by running the following command in your terminal
	docker exec -it redis redis-cli
9. In the Redis CLI, you can test if Redis is working by setting a key-value pair
	set testkey "Hello, Redis!"
10. To retrieve the value you just set, use
	get testkey
11. You should see the output
	"Hello, Redis!"
12. If you want to stop the Redis container, run the following command
	docker stop redis
13. If you need to start the Redis container again, run
	docker start redis
14. If you want to remove the Redis container completely, use the following command
	docker rm redis
15. If you no longer need the Redis image, you can remove it using
	docker rmi redis
16. Docker containers don’t start automatically when your system boots. However, you can configure Docker to restart the Redis container automatically on system reboot.
	When you run the Redis container, you can add the --restart flag
	docker run --name redis -p 6379:6379 -d --restart always redis




