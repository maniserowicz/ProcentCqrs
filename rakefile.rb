# required gems
require 'rubygems'
require 'albacore'
require 'rake/clean'

def log(msg)
    puts "***", msg, "***"
end

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
task :default => [:clean, :build, :publish]

# configuration for rake/clean task
CLEAN.include (OUTPUT)
CLEAN.include (FileList["src/**/#{CONFIGURATION}"])

# 'build' task
desc "builds solution"
msbuild :build => :asminfo do |msb|
    log "starting build"
    msb.properties :configuration => CONFIGURATION
    msb.targets :Clean, :Rebuild
    msb.solution = SLN_FILE
    msb.verbosity = "q"
end

# 'asminfo' task
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