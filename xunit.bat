set xunit_path=.\tools\xunit-1.8\xunit.console.clr4.exe
set test_dir=.\src\Tests\bin\Debug\
set test_dll_path=%test_dir%ProcentCqrs.Tests.dll

%xunit_path% %test_dll_path%