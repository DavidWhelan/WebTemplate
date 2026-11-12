import { defineConfig } from 'vite';
import fs from 'fs';
import path from 'path';
import child_process from 'child_process';
import { env } from 'process';
import viteRequestLogger from 'vite-plugin-request-logger';
import fable from 'vite-plugin-fable'

const baseFolder =
    env.APPDATA !== undefined && env.APPDATA !== ''
        ? `${env.APPDATA}/ASP.NET/https`
        : `${env.HOME}/.aspnet/https`;

const certificateName = "WebTemplate";
const certFilePath = path.join(baseFolder, `${certificateName}.pem`);
const keyFilePath = path.join(baseFolder, `${certificateName}.key`);

if (!fs.existsSync(baseFolder)) {
    fs.mkdirSync(baseFolder, { recursive: true });
}

if (!fs.existsSync(certFilePath) || !fs.existsSync(keyFilePath)) {
    if (0 !== child_process.spawnSync('dotnet', [
        'dev-certs',
        'https',
        '--export-path',
        certFilePath,
        '--format',
        'Pem',
        '--no-password',
    ], { stdio: 'inherit', }).status) {
        throw new Error("Could not create certificate.");
    }
}

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [viteRequestLogger({ prefix: "/" }), fable()],
    appType: 'custom',
    publicDir: 'public',
    build: {
        emptyOutDir: true,
        outDir: '../Server/wwwroot',
        assetsDir: '',
        rolldownOptions: {
            input: ['js/datastar.js', 'assets/app.css', 'Components/ClickCounter.fs'],
            output: {
                entryFileNames: (chunkInfo) => {
                    if (chunkInfo.facadeModuleId?.endsWith(".fs") && chunkInfo.facadeModuleId?.includes("Components")) {
                        return "Components/[name].fs"
                    }
                    else if (chunkInfo.facadeModuleId?.endsWith(".fs")) {
                        return "fs/[name].fs"
                    }
                    else {
                        return "js/[name].js"
                    }
                    
                },
                assetFileNames: `assets/[name].[ext]`
            }
        },
    },
    server: {
        strictPort: true,
        https: {
            key: fs.readFileSync(keyFilePath),
            cert: fs.readFileSync(certFilePath),
        }
    }
})