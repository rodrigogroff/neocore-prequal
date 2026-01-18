call npm run build

xcopy ".\src\js\server.js" .\dist /E /D /S /I /Q /Y /F
xcopy ".\src\js\routes.js" .\dist /E /D /S /I /Q /Y /F
xcopy ".\src\js\scripts\*.js" .\dist\src /E /D /S /I /Q /Y /F
xcopy "favicon.ico" .\dist /Y /F
xcopy .\src\html .\dist /E /D /S /I /Q /Y /F
xcopy .\src\img .\dist\src\img /E /D /S /I /Q /Y /F
xcopy .\src\vendor .\dist\src\vendor /E /D /S /I /Q /Y /F
xcopy .\src\files .\dist\src\files /E /D  /S /I /Q /Y /F

start node dist/server.js &
