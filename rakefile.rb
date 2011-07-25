# required gems
require 'rubygems'
require 'albacore'
require 'rake/clean'

# define build variables
OUTPUT = 'Output'
CONFIGURATION = 'Release'
SLN_FILE = 'src/ProcentCqrs.sln'
VERSION = '0.1.0.0'

# set albacore configuration variables
Albacore.configure do |config|
    config.log_level = :verbose
    config.msbuild.use :net4
end

# introduce custom build targets

desc "Compiles solution, runs unit tests and puts all products in #{OUTPUT} dir"
task :default => [:clean, :build, :test, :publish]

desc "Runs all unit tests"
task :test => [:mspec, :xunit]

# configuration for rake/clean task
CLEAN.include (OUTPUT)
CLEAN.include (FileList["src/**/#{CONFIGURATION}"])

desc "builds solution"
msbuild :build => [:clean, :asminfo] do |msb|
    msb.properties :configuration => CONFIGURATION
    msb.targets :Clean, :Rebuild
    msb.solution = SLN_FILE
    msb.verbosity = "q"
end

desc "generates SharedAssemblyInfo.cs file"
assemblyinfo :asminfo do |asm|
    asm.version = VERSION
    asm.company_name = "Maciej Aniserowicz"
    asm.title = "Procent.CQRS"
    asm.product_name = "Procent.CQRS"
    asm.description = "Playground for experiments with CQRS in ASP.NET MVC (and some other useful stuff like knockout.js, rake etc...)"
    asm.copyright = "Copyright (C) Maciej Aniserowicz"
    asm.output_file = 'src/SharedAssemblyInfo.cs'
end

desc "copies all output files to a directory #{OUTPUT}"
task :publish => :build do
    Dir.mkdir(OUTPUT)
    FileUtils.cp_r FileList["src/**/#{CONFIGURATION}/*.dll", "src/**/#{CONFIGURATION}/*.pdb"].exclude(/obj\//).exclude(/.Tests/), OUTPUT
end

desc "runs MSpec tests"
mspec :mspec => :build do |mspec|
    mspec.command = "tools/Machine.Specifications-Release.0.4.115/mspec-clr4.exe"
    #mspec.html_output = "./mspec_results.html"
    mspec.assemblies "src/Tests/bin/#{CONFIGURATION}/ProcentCqrs.Tests.dll"
end

desc "runs xUnit tests"
xunit :xunit => :build do |xunit|
    xunit.command = "tools/xunit-1.8/xunit.console.clr4.exe"
    #xunit.html_output = "."
    xunit.assemblies = FileList["src/**/#{CONFIGURATION}/*.Tests.dll"].exclude(/obj\//)
end