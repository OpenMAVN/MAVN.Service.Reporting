#!/bin/bash
ReplaceWhat='<Version>.*</Version>'
BuildVersion="<Version>$1<\/Version>"
ReplacementString="s@${ReplaceWhat}@${BuildVersion}@g"
FILES=$(find -type f -name '*.csproj')
echo $ReplaceWhat
echo $BuildVersion
for file in $FILES; do
    foundVersion=$(grep $ReplaceWhat $file)
    if [ "$foundVersion" ]; then
        sed -i $ReplacementString $file
    echo $file
    else
        echo "Not found Version property in ${file}"
        exit 1
    fi
done