# required gems
require 'rubygems'
require 'albacore'
require 'rake/clean'

# define build variables
OUTPUT = 'Output'
CONFIGURATION = 'Release'
SLN_FILE = 'src/ProcentCqrs.sln'
VERSION = '0.1.0.0'
WEBAPP_NAME = 'Web.Mvc'
WEBAPP_DIR = "src/#{WEBAPP_NAME}"
WEBAPP_PROJFILE = "#{WEBAPP_DIR}/#{WEBAPP_NAME}.csproj"

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
    msb.properties :configuration => CONFIGURATION, :MvcBuildViews => "true"
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

desc "transforms web.config"
msbuild :transformwebconfig do |msb|
    msb.properties :configuration => CONFIGURATION
    msb.targets :TransformWebConfig
    msb.solution = WEBAPP_PROJFILE
    msb.verbosity = "q"
end

desc "copies all output files to a directory #{OUTPUT}, ready for xcopy via ftp"
task :publish => [:build, :transformwebconfig] do
    #creating dir structure
    Dir.mkdir(OUTPUT)
    Dir.mkdir("#{OUTPUT}/bin")
    
    #copying binaries to /bin dir
    FileUtils.cp_r FileList["src/**/#{CONFIGURATION}/*.dll",
        "src/**/#{CONFIGURATION}/*.pdb"]
        .exclude(/obj\//)
        .exclude(/.Tests/),
        "#{OUTPUT}/bin"

    # "#{WEBAPP_DIR}/**/." - created files in correct directories as well as in root dir
    # "#{WEBAPP_DIR}/**" - works as expected (creating files only in subdirs, not duplicating in root)
    FileUtils.cp_r FileList["#{WEBAPP_DIR}/**"]
        .exclude(/\/obj/)
        .exclude(/\/bin/)
        .exclude(/web\.[^\/]*config/i)
        .exclude(/T4MVC.*/)
        .exclude(/\/logs/), "#{OUTPUT}"
    
    FileUtils.rm FileList["#{OUTPUT}/**/*.cs"]
    FileUtils.rm FileList["#{OUTPUT}/**/*.csproj"]
    FileUtils.rm FileList["#{OUTPUT}/**/*.csproj.user"]

    #copying transformed web.config to
    transformedConfig = "#{WEBAPP_DIR}/obj/#{CONFIGURATION}/TransformWebConfig/transformed/web.config"
    FileUtils.cp transformedConfig, "#{OUTPUT}"
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