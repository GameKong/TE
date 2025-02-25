#!/bin/bash

[ -d Luban ] && rm -rf Luban

dotnet build  ./LubanSrc/src/Luban/Luban.csproj -c Release -o Luban