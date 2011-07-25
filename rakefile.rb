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
msbuild :build do |msb|
    log "starting build"
    msb.properties :configuration => CONFIGURATION
    msb.targets :Clean, :Rebuild
    msb.solution = SLN_FILE
    msb.verbosity = "q"
end