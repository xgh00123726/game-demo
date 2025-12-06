import sys

workspace = sys.argv[1]
settings_file_path = f'{workspace}/.vscode/settings.json'
is_visible = False

with open(settings_file_path, 'r', encoding='utf-8') as f:
    flines = f.readlines()

with open(settings_file_path, 'w') as f:
    for line in flines:
        if '**/*.meta' in line:
            if 'true' in line:
                line = line.replace('true', 'false')
                is_visible = True
            else:
                line = line.replace('false', 'true')
                is_visible = False
        f.write(line)

print(f'settings 文件替换完成')
print(f'meta文件现在{'可见' if is_visible else '不可见'}')