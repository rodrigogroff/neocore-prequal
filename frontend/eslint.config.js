module.exports = [
  {
    ignores: ['node_modules/', 'dist/', '*.config.js', 'src/vendor/**'],
  },
  {
    files: ['src/**/*.js'],
    languageOptions: {
      ecmaVersion: 2020,
      sourceType: 'module',
      globals: {
        console: 'readonly',
        window: 'readonly',
        document: 'readonly',
        process: 'readonly',
        module: 'readonly',
        require: 'readonly',
        localStorage: 'readonly',
        FormData: "readonly",
        sessionStorage: 'readonly',
        location: 'readonly',
        setInterval: 'readonly',
        clearInterval: 'readonly',
        setTimeout: 'readonly',
        fetch: 'readonly',
        __dirname: 'readonly',
        $: 'readonly',
        jQuery: 'readonly',
      },
    },
    rules: {
      'no-undef': 'error',
      'no-unused-vars': 'warn',
      'no-console': 'off',
    },
  },
];