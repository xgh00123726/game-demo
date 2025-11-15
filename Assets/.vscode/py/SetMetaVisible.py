import sys

workspace = sys.argv[1]
is_visible = sys.argv[2] == 'meta文件可见'
is_visible = str(not is_visible).lower()
settings_file_path = f'{workspace}/.vscode/settings.json'


with open(settings_file_path, 'r', encoding='utf-8') as f:
    flines = f.readlines()

with open(settings_file_path, 'w') as f:
    for line in flines:
        if '**/*.meta' in line:
            line = line.replace('true', is_visible).replace('false', is_visible)
        f.write(line)

is_visible_cn = '不可见' if is_visible == 'true' else '可见'
print(f'meta 文件替换完成')
print(f'现在meta文件已经{is_visible_cn}')