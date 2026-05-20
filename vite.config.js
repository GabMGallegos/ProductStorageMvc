import { defineConfig } from 'vite';

export default defineConfig({
    root: 'frontend',
    build: {
        outDir: '../wwwroot/dist',
        emptyOutDir: true,
        assetsDir: '',
        rollupOptions: {
            input: './src/main.js',
            output: {
                entryFileNames: 'main.js',
                chunkFileNames: '[name].js',
                assetFileNames: 'main.css'
            }
        }
    }
});
