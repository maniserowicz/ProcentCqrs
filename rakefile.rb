# required gems
require 'rubygems'
require 'albacore'

# define build variables
OUTPUT = 'build'
CONFIGURATION = 'Release'
SLN_FILE = 'src/ProcentCqrs.sln'

# set albacore configuration variables
Albacore.configure do |config|
    config.log_level = :verbose
    config.msbuild.use :net4
end