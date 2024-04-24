<template>
    <div class="container mx-auto p-4 sm:p-6">
        <div v-for="person in people" :key="person.id" class="flex flex-wrap justify-between items-top space-x-2 pb-2 pt-2 border-b-2">
            <img src="../assets/vedun.jpg"
                 alt="user-image"
                 class="max-w-32 h-32 max-h-32 rounded-full" />
            <div>
                <div class="bg-green-800 font-semibold text-white p-2">
                    {{ person.fullName }}
                </div>
                <div class="max-w-3xl p-2">
                    {{ person.description }}
                </div>
                <div class="flex border-b-2 p-2">
                    <div class="flex border-b-2 pb-2 m-2 space-x-2">
                        <img src="../icons/history.png" alt="Иконка истории" class="h-6 mr-2">
                        <div>{{ formatDate(person.date) }}</div>
                    </div>

                    <div class="flex border-b-2 pb-2 m-2 space-x-2">
                        <img src="../icons/comment.png" alt="Иконка комментариев" class="h-6 mr-2">
                        <div>{{ person.yearWork }} лет</div>
                    </div>
                </div>
            </div>
            <div>
                <div class="bg-green-800 font-semibold text-white p-2">Soft Скиллы</div>
                <div class="p-2">
                    <div class="pb-2">
                        {{ person.softSkills }}
                    </div>
                </div>
            </div>
            <div>
                <div class="bg-green-800 font-semibold text-white p-2">Hard Скиллы</div>
                <div class="p-2">
                    {{ person.hardSkills }}
                </div>
            </div>
            <div>
                <div class="bg-green-800 font-semibold text-white p-2">
                    Опыт и образование
                </div>
                <div class="p-2">
                    <div>{{ person.experience }}</div>
                </div>
            </div>
        </div>
        <div v-if="people.length === 0" class="text-center text-white bg-red-500 p-2">
            No data available
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
                people: [],
                showModal: false
            };
        },
        mounted() {
            this.loadPeople();
        },
        methods: {
            async loadPeople() {
                try {
                    const response = await axios.get('https://localhost:7175/api/AuthProcess/getAllPersons');
                    this.people = response.data;
                } catch (error) {
                    console.error("Error loading person data: ", error);
                }
            },

            formatDate(date) {
                return new Date(date).toLocaleDateString();
            },
            openModal(index) {
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
