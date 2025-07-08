declare @token nvarchar(500)

set @token = 'dbo|DayOfTheWeek'
EXEC SYS3.InitializeLookup 'dbo', 'DayOfTheWeek', 'جدول زمان بندی'

EXEC SYS3.AddLookupValue @token, 1, N'شنبه'
EXEC SYS3.AddLookupValue @token, 2, N'یکشنبه'
EXEC SYS3.AddLookupValue @token, 3, N'دوشنبه'
EXEC SYS3.AddLookupValue @token, 4, N'سه شنبه'
EXEC SYS3.AddLookupValue @token, 5, N'چهارشنبه'
EXEC SYS3.AddLookupValue @token, 6, N'پنجشنبه'
EXEC SYS3.AddLookupValue @token, 7, N'جمعه'
