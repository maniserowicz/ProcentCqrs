set mspec_path=.\lib\Machine.Specifications-Release.0.4.13.0\mspec-clr4.exe
set test_dir=.\src\Tests\bin\Debug\
set test_dll_path=%test_dir%ProcentCqrs.Tests.dll
set html_output_path=%test_dir%mspec-output.html

%mspec_path% %test_dll_path% --html %html_output_path%

start %html_output_path%