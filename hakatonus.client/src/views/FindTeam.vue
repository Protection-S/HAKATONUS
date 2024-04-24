<template>
    <div class="container mx-auto p-4 sm:p-6">
        <div v-if="teams.length > 0">
            <div v-for="team in teams" :key="team.id" class="flex flex-wrap justify-between items-top space-x-2 pb-2 pt-2 border-b-2">
                            <img src="../assets/vedun.jpg"

                     alt="team-image"
                     class="max-w-32 h-32 max-h-32 m-2 rounded-full" />
                <div class="mb-2">
                    <div class="bg-green-800 font-semibold text-white p-2">{{ team.name }}</div>
                    <div class="p-2 max-w-screen-md">{{ team.description }}</div>
                    <div class="flex border-b-2 p-2">
                        <div class="flex border-b-2 pb-2 m-2 space-x-2">
                            <Icon name="mdi:calendar" width="24" height="24" fill="black" />
                            <div>{{ team.date }}</div>
                        </div>
                        <div class="flex border-b-2 pb-2 m-2 space-x-2">
                            <Icon name="mdi:eye" width="24" height="24" fill="black" />
                            <div>{{ team.exp }} лет</div>
                        </div>
                    </div>
                </div>
                <div>
                    <div class="bg-green-800 font-semibold text-white p-2">Кто нужен</div>
                    <div class="p-2">
                        <div class="pb-2" v-html="team.whoNeed"></div>
                    </div>
                </div>
                <div>
                    <div class="bg-green-800 font-semibold text-white p-2">Стек</div>
                    <div class="p-2">
                        <div class="font-semibold">Инструменты:</div>
                        <div class="pb-2" v-html="team.instruments"></div>
                        <div class="font-semibold">Языки программирования:</div>
                        <div v-html="team.language"></div>
                    </div>
                </div>
                <div>
                    <div class="bg-green-800 font-semibold text-white p-2">Сроки</div>
                    <div class="p-2">
                        <div class="font-semibold">Выполнение от:</div>
                        <div>{{ team.dateStart }}</div>
                    </div>
                    <div class="p-2">
                        <div class="font-semibold">Выполнение до:</div>
                        <div>{{ team.dateEnd }}</div>
                    </div>
                </div>

            </div>
            <div class="bg-green-800 font-semibold text-center text-white p-2">
                <button @click="openModal(index)">Подробная информация</button>
            </div>
        </div>
        <div v-else class="text-center text-white bg-red-500 p-2">
            Нет доступных данных
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
            teams: [],
            showModal: false
        };
    },
    mounted() {
        this.loadTeams();
    },
    methods: {
        async loadTeams() {
            try {
                const response = await axios.get('https://localhost:7175/api/AuthProcess/getAllTeams');
                this.teams = response.data;
            } catch (error) {
                console.error("Ошибка при загрузке данных о командах: ", error);
            }
        },
        openModal() {
            this.showModal = true;
        },
        closeModal() {
            this.showModal = false;
        }
    }
}
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
