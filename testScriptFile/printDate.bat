@echo off

echo STANDARD OUTPUT - %DATE% - %TIME%

ping 192.0.2.2 -n 1 -w 10000 > nul

REM echo STANDARD ERROR - %DATE% - %TIME% 1>&2