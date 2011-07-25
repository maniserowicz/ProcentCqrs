# required gems
require 'rubygems'
require 'albacore'
require 'rake/clean'

def log(msg)
    puts "***", msg, "***"
end

# define build variables
CONFIGURATION = 'Release'
SLN_FILE = 'src/ProcentCqrs.sln'
VERSION = '0.1.0.0'

# set albacore configuration variables
Albacore.configure do |config|
    config.log_level = :verbose
    config.msbuild.use :net4
end

# introduce custom build targets
desc "Compiles solution and runs unit tests"
task :default => [:clean, :build]

# configuration for rake/clean task
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
