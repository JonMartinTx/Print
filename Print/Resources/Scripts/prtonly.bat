:@echo off
: Run the AS400Report tool
cd "C:\AS400Report\AS400Report\bin\Debug"
start AS400Report Reports
cd "C:\AS400Report\Print Queue\prtq"
echo Processing complete.