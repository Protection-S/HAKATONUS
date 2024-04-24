<template>
    <div class="container mx-auto p-4 sm:p-6">
        <div v-for="investor in investors" :key="investor.id" class="flex flex-wrap justify-evenly items-top space-x-2 pb-2 pt-2 border-b-2">
            <img src="../assets/vedun.jpg"
                 alt="user-image" class="max-w-32 h-32 max-h-32 m-2 rounded-full" />
            <div class="mb-2">
                <div class="bg-green-800 font-semibold text-white p-2">
                    {{ investor.fullName }}
                </div>
                <div class="p-2 max-w-screen-md">
                    {{ investor.description }}
                </div>
                <div class="flex border-b-2 p-2">
                    <div class="flex border-b-2 pb-2 m-2 space-x-2">
                        <Icon name="mdi:calendar" width="24" height="24" fill="black" />
                        <div>{{ new Date(investor.date).toLocaleDateString() }}</div>
                    </div>
                    <div class="flex border-b-2 pb-2 m-2 space-x-2">
                        <Icon name="arcticons:zoho-projects" width="24" height="24" fill="black" />
                        <div>Спонсированных проектов:</div>
                        <div>{{ investor.projectCount }}</div>
                    </div>
                </div>
            </div>
            <div>
                <div class="bg-green-800 font-semibold text-white p-2">Основатель</div>
                <div class="p-2">
                    <div class="pb-2">{{ investor.owner }}</div>
                </div>
            </div>
            <div>
                <div class="bg-green-800 font-semibold text-white p-2">
                    Топ спонсированных проектов
                </div>
                <div class="p-2 space-x-2 flex justify-between">
                    <div v-for="(project, index) in investor.sponsorProject.split(',')" :key="index" class="font-semibold">
                        {{ project }}
                    </div>
                </div>
            </div>
        </div>
        <div v-if="investors.length === 0" class="text-center text-xl">
            Нет данных о инвесторах.
        </div>
        <div class="bg-green-800 font-semibold text-center text-white p-2">
            <button @click="openModal(index)">Подробная информация</button>
        </div>
    </div>

    <div v-if="showModal" class="fixed inset-0 flex items-center justify-center bg-gray-900 bg-opacity-50">
        <div class="bg-white p-6 rounded-lg">
            <p>В БЕТА ТЕСТЕ</p>
            <button @click="closeModal" class="mt-4 bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded">Закрыть</button>
        </div>
    </div>
</template>

<script>
    import axios from 'axios';

    export default {
        data() {
            return {
                investors: [],
                showModal: false
            };
        },
        created() {
            this.fetchInvestors();
        },
        methods: {
            fetchInvestors() {
                axios.get('https://localhost:7175/api/AuthProcess/getAllInvestors')
                    .then(response => {
                        this.investors = response.data;
                    })
                    .catch(error => {
                        console.error('Ошибка:', error);
                        this.investors = [];
                    });
            },
            openModal(index) {
                this.showModal = true;
            },
            closeModal() {
                this.showModal = false;
            }
        }
    };
</script>

<style scoped>
    @tailwind base;
    @tailwind components;
    @tailwind utilities;

    .container {
        overflow-y: auto;
        max-height: 70vh;
    }
</style>
